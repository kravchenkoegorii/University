#pragma once

template <typename T>
class Node {
public:
	T value;
	Node* child;
	Node* neighbour;
	Node(T, Node* = nullptr, Node* = nullptr);
};

template <typename T>
Node<T>::Node(T _value, Node* _child, Node* _neighbour) {
	value = _value;
	child = _child;
	neighbour = _neighbour;
}