#pragma once
#include <iostream>
#include <numeric>
#include <vector>
#include "node.h"

using namespace std;

template <typename T>
class Multilist {
	Node<T>* root;
	vector<int> sizes;
	int size;
public:
	Multilist();
	~Multilist();
	Multilist<T>& copy();
	void print();
	bool is_empty();
	int get_size();
	int get_level_size(int _level);
	void add(T _value, T _search_value, int _neighbour);
	void clear();
	bool clear_level(int _level);
	bool clear_branch_by_value(T _value);
private:
	void clear(Node<T>*& _node);
	void clear_branch(Node<T>*& _node, int _level);
	Node<T>* find_clear_point(int& _level, Node<T>*& _node, int _current_level);
	pair<Node<T>*, int> find_node(T& _value, Node<T>*& _node, int _level);
	void copy(Node<T>*& _newNode, Node<T>*& _node);
	void print_node(Node<T>*& _node, int _level);
};

template <typename T>
Multilist<T>::Multilist() {
	root = nullptr;
	size = 0;
}

template <typename T>
Multilist<T>::~Multilist() {
	clear();
}

template <typename T>
void Multilist<T>::clear() {
	clear(root);
	root = nullptr;
	size = 0;
	sizes.clear();
}

template<typename T>
bool Multilist<T>::is_empty() {
	return root == nullptr;
}

template<typename T>
int Multilist<T>::get_size() {
	return size;
}

template<typename T>
int Multilist<T>::get_level_size(int _level) {
	return sizes.at(_level - 1);
}

template<typename T>
void Multilist<T>::add(T _value, T _search_value, int _neighbour) {
	if (root == nullptr) {
		root = new Node<T>(_value);
		size++;
		sizes.push_back(1);
		return;
	}

	auto result = find_node(_search_value, root, 1);

	Node<T>* node = result.first;

	if (node == nullptr) {
		return;
	}
		
	if (_neighbour == 1) {
		Node<T>* new_node = new Node<T>(_value);
		new_node->neighbour = node->neighbour;
		node->neighbour = new_node;
		size++;
		sizes[result.second - 1]++;
		return;
	}

	if (node->child) {
		node = node->child;
		while (node->neighbour != nullptr)
			node = node->neighbour;
		node->neighbour = new Node<T>(_value);
		size++;
		sizes[result.second]++;
		return;
	}

	node->child = new Node<T>(_value);
	size++;

	try {
		sizes.at(result.second)++;
	}
	catch (exception const& exc) {
		sizes.push_back(1);
	}
}

template<typename T>
pair<Node<T>*, int> Multilist<T>::find_node(T& _value, Node<T>*& _node, int _level) {
	if (_node == nullptr) {
		return make_pair(nullptr, -1);
	}

	if (_node->value == _value) {
		return make_pair(_node, _level);
	}
		
	auto result = find_node(_value, _node->child, _level + 1);

	if (result.first != nullptr) {
		return result;
	}

	result = find_node(_value, _node->neighbour, _level);

	if (result.first != nullptr) {
		return result;
	}

	return make_pair(nullptr, -1);
}

template<typename T>
Node<T>* Multilist<T>::find_clear_point(int& _level, Node<T>*& _node, int _current_level) {
	if (_node == nullptr) {
		return nullptr;
	}

	if (_node->child && _level == (_current_level + 1)) {
		return _node;
	}

	Node<T>* node = find_clear_point(_level, _node->child, _current_level + 1);

	if (node != nullptr) {
		return node;
	}

	node = find_clear_point(_level, _node->neighbour, _current_level);

	if (node != nullptr) {
		return node;
	}
}

template<typename T>
void Multilist<T>::copy(Node<T>*& _newNode, Node<T>*& _node) {
	if (_node->child) {
		_newNode->child = new Node<T>(_node->child->value);
		copy(_newNode->child, _node->child);
	}

	if (_node->neighbour) {
		_newNode->neighbour = new Node<T>(_node->neighbour->value);
		copy(_newNode->neighbour, _node->neighbour);
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

	multilist->size = size;
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
void Multilist<T>::print_node(Node<T>*& _node, int _level) {
	for (int i = 0; i < _level; i++) {
		cout << "=== ";
	}

	cout << _node->value;

	cout << endl;

	if (_node->child) {
		print_node(_node->child, _level + 1);
	}
	if (_node->neighbour) {
		print_node(_node->neighbour, _level);
	}
}

template <typename T>
void Multilist<T>::clear(Node<T>*& _node) {
	if (_node) {
		clear(_node->child);
		clear(_node->neighbour);
		delete _node;
	}
}

template<typename T>
bool Multilist<T>::clear_level(int _level) {
	if (sizes.size() < _level) {
		return false;
	}

	if (_level == 1) {
		clear();
		return true;
	}

	Node<T>* node = find_clear_point(_level, root, 1);

	while (node != nullptr) {
		clear(node->child);
		node->child = nullptr;
		node = node->neighbour;
	}

	sizes.erase(sizes.begin() + _level - 1, sizes.end());
	size = accumulate(sizes.begin(), sizes.end(), 0);

	return true;
}

template<typename T>
bool Multilist<T>::clear_branch_by_value(T _value) {
	auto result = find_node(_value, root, 1);

	Node<T>* node = result.first;

	if (node == nullptr || node->child == nullptr) {
		return false;
	}

	clear_branch(node->child, result.second + 1);
	node->child = nullptr;
}

template<typename T>
void Multilist<T>::clear_branch(Node<T>*& _node, int _level) {
	if (_node) {
		clear_branch(_node->child, _level + 1);
		clear_branch(_node->neighbour, _level);
		delete _node;

		if (sizes[_level - 1] == 1) {
			sizes.erase(sizes.begin() + _level - 1);
		}
		else {
			sizes[_level - 1]--;
		}

		size--;
	}
}