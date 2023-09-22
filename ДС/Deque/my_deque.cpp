#include "my_deque.h"

template <typename T>
MyDeque<T>::MyDeque() {
    front = nullptr;
    rear = nullptr;
    size = 0;
}

template <typename T>
void MyDeque<T>::addFront(T val) {
    Node<T>* newNode = new Node<T>(val);
    if (isEmpty()) {
        front = rear = newNode;
    }
    else {
        newNode->next = front;
        front->prev = newNode;
        front = newNode;
    }
    size++;
}

template <typename T>
void MyDeque<T>::addRear(T val) {
    Node<T>* newNode = new Node<T>(val);
    if (isEmpty()) {
        front = rear = newNode;
    }
    else {
        newNode->prev = rear;
        rear->next = newNode;
        rear = newNode;
    }
    size++;
}

template <typename T>
T MyDeque<T>::removeFront() {
    if (isEmpty()) {
        return NULL;
    }
    Node<T>* temp = front;
    T val = temp->data;
    front = front->next;
    if (front) {
        front->prev = nullptr;
    }
    else {
        rear = nullptr;
    }
    delete temp;
    size--;
    return val;
}

template <typename T>
T MyDeque<T>::removeRear() {
    if (isEmpty()) {
        return NULL;
    }
    Node<T>* temp = rear;
    T val = temp->data;
    rear = rear->prev;
    if (rear) {
        rear->next = nullptr;
    }
    else {
        front = nullptr;
    }
    delete temp;
    size--;
    return val;
}

template <typename T>
T MyDeque<T>::peekFront() {
    if (isEmpty()) {
        return NULL;
    }
    return front->data;
}

template <typename T>
T MyDeque<T>::peekRear() {
    if (isEmpty()) {
        return NULL;
    }
    return rear->data;
}

template <typename T>
void MyDeque<T>::swapFrontAndRear() {
    if (size < 2) {
        return;
    }

    Node<T>* firstNode = front;
    Node<T>* lastNode = rear;

    lastNode->next = firstNode->next;
    firstNode->next = nullptr;
    rear = firstNode;
    rear->next = nullptr;

    front = lastNode;
    front->next = nullptr;
}

template <typename T>
bool MyDeque<T>::isEmpty() const {
    return size == 0;
}

template <typename T>
int MyDeque<T>::getSize() const {
    return size;
}

template <typename T>
bool MyDeque<T>::contains(T val) const {
    Node<T>* current = front;
    while (current) {
        if (current->data == val) {
            return true;
        }
        current = current->next;
    }
    return false;
}

template <typename T>
void MyDeque<T>::reverse() {
    if (size < 2) {
        return;
    }

    Node<T>* current = front;
    while (current) {
        Node<T>* temp = current->next;
        current->next = current->prev;
        current->prev = temp;

        current = temp;
    }

    Node<T>* temp = front;
    front = rear;
    rear = temp;
}

template <typename T>
void MyDeque<T>::clear() {
    while (!isEmpty()) {
        removeFront();
    }
}

template <typename T>
void MyDeque<T>::printDeque() {
    Node<T>* current = front;

    std::cout << "Ålements: ";
    while (current != nullptr) {
        std::cout << current->data << " ";
        current = current->next;
    }
    std::cout << std::endl;
}

template <typename T>
MyDeque<T>::~MyDeque() {
    clear();
}