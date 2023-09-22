#pragma once
#include <iostream>
#include "node.h"

template <typename T>
class MyDeque {
private:
    Node<T>* front;
    Node<T>* rear;
    int size;
public:
    MyDeque();
    ~MyDeque();
    void addFront(T val);
    void addRear(T val);
    T removeFront();
    T removeRear();
    T peekFront();
    T peekRear();
    void swapFrontAndRear();
    bool isEmpty() const;
    int getSize() const;
    bool contains(T val) const;
    void clear();
    void reverse();
    void printDeque();
};