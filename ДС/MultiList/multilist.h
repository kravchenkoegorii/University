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
public:
	Multilist();
	~Multilist();
	Multilist<T>& copy();
	void print();
	bool is_empty();
	int get_size();
	int get_level_size(int _level);
	void add(T _value, T _search_value, string _isNeighbour);
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