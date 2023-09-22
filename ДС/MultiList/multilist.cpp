#include "multilist.h"

template <typename T>
Multilist<T>::Multilist() {
	root = nullptr;
}

template <typename T>
Multilist<T>::~Multilist() {
	clear();
}

template <typename T>
void Multilist<T>::clear() {
	clear(root);
	root = nullptr;
	sizes.clear();
}

template<typename T>
bool Multilist<T>::is_empty() {
	return root == nullptr;
}

template<typename T>
int Multilist<T>::get_size() {
	return accumulate(sizes.begin(), sizes.end(), 0);
}

template<typename T>
int Multilist<T>::get_level_size(int level) {
	return sizes.at(level - 1);
}

template<typename T>
void Multilist<T>::add(T value, T search_value, string isNeighbour) {
	if (root == nullptr) {
		root = new Node<T>(value);
		sizes.push_back(1);
		return;
	}

	auto result = find_node(search_value, root, 1);

	Node<T>* node = result.first;

	if (node == nullptr) {
		return;
	}

	if (isNeighbour == "n") {
		Node<T>* new_node = new Node<T>(value);
		new_node->neighbour = node->neighbour;
		node->neighbour = new_node;
		sizes[result.second - 1]++;
		return;
	}

	if (node->child) {
		node = node->child;
		while (node->neighbour != nullptr)
			node = node->neighbour;
		node->neighbour = new Node<T>(value);
		sizes[result.second]++;
		return;
	}

	node->child = new Node<T>(value);

	try {
		sizes.at(result.second)++;
	}
	catch (exception ex) {
		sizes.push_back(1);
	}
}

template<typename T>
pair<Node<T>*, int> Multilist<T>::find_node(T& value, Node<T>*& node, int level) {
	if (node == nullptr) {
		return make_pair(nullptr, -1);
	}

	if (node->value == value) {
		return make_pair(node, level);
	}

	auto result = find_node(value, node->child, level + 1);

	if (result.first != nullptr) {
		return result;
	}

	result = find_node(value, node->neighbour, level);

	if (result.first != nullptr) {
		return result;
	}

	return make_pair(nullptr, -1);
}

template<typename T>
Node<T>* Multilist<T>::find_clear_point(int& level, Node<T>*& _node, int current_level) {
	if (_node == nullptr) {
		return nullptr;
	}

	if (_node->child && level == (current_level + 1)) {
		return _node;
	}

	Node<T>* node = find_clear_point(level, _node->child, current_level + 1);

	if (_node != nullptr) {
		return _node;
	}

	_node = find_clear_point(level, _node->neighbour, current_level);

	if (_node != nullptr) {
		return _node;
	}
}

template<typename T>
void Multilist<T>::copy(Node<T>*& newNode, Node<T>*& node) {
	if (node->child) {
		newNode->child = new Node<T>(node->child->value);
		copy(newNode->child, node->child);
	}

	if (node->neighbour) {
		newNode->neighbour = new Node<T>(node->neighbour->value);
		copy(newNode->neighbour, node->neighbour);
	}
}

template<typename T>
Multilist<T>& Multilist<T>::copy() {
	if (root == nullptr) {
		return *new Multilist<T>();
	}

	Multilist<T>* multilist = new Multilist<T>();

	multilist->root = new Node<T>(root->value);

	copy(multilist->root, root);

	multilist->sizes = sizes;

	return *multilist;
}

template<typename T>
void Multilist<T>::print() {
	if (root == nullptr) {
		cout << "Multilist is empty\n";
		return;
	}
	print_node(root, 0);
	cout << endl;
}

template<typename T>
void Multilist<T>::print_node(Node<T>*& node, int level) {
	for (int i = 0; i < level; i++) {
		cout << "=== ";
	}

	cout << node->value;

	cout << endl;

	if (node->child) {
		print_node(node->child, level + 1);
	}
	if (node->neighbour) {
		print_node(node->neighbour, level);
	}
}

template <typename T>
void Multilist<T>::clear(Node<T>*& node) {
	if (node) {
		clear(node->child);
		clear(node->neighbour);
		delete node;
	}
}

template<typename T>
bool Multilist<T>::clear_level(int level) {
	if (sizes.size() < level) {
		return false;
	}

	if (level == 1) {
		clear();
		return true;
	}

	Node<T>* node = find_clear_point(level, root, 1);

	while (node != nullptr) {
		clear(node->child);
		node->child = nullptr;
		node = node->neighbour;
	}

	sizes.erase(sizes.begin() + level - 1, sizes.end());
	size = accumulate(sizes.begin(), sizes.end(), 0);

	return true;
}

template<typename T>
bool Multilist<T>::clear_branch_by_value(T value) {
	auto result = find_node(value, root, 1);

	Node<T>* node = result.first;

	if (node == nullptr || node->child == nullptr) {
		return false;
	}

	clear_branch(node->child, result.second + 1);
	node->child = nullptr;
}

template<typename T>
void Multilist<T>::clear_branch(Node<T>*& node, int level) {
	if (node) {
		clear_branch(node->child, level + 1);
		clear_branch(node->neighbour, level);
		delete node;

		if (sizes[level - 1] == 1) {
			sizes.erase(sizes.begin() + level - 1);
		}
		else {
			sizes[level - 1]--;
		}
	}
}