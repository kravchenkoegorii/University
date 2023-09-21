#include <iostream>
#include "my_deque.h"

void print() {
    std::cout << "1. Add an element to the beginning" << std::endl;
    std::cout << "2. Add an element to the end" << std::endl;
    std::cout << "3. Delete the first element" << std::endl;
    std::cout << "4. Delete the last element" << std::endl;
    std::cout << "5. Read the first element" << std::endl;
    std::cout << "6. Read the last element" << std::endl;
    std::cout << "7. Swap the first and last elements" << std::endl;
    std::cout << "8. Check if the deck is empty" << std::endl;
    std::cout << "9. Get the size" << std::endl;
    std::cout << "10. Reverse deck" << std::endl;
    std::cout << "11. Check whether the given element belongs to the deck" << std::endl;
    std::cout << "12. Clear the deck" << std::endl;
    std::cout << "13. Exit the program" << std::endl;
}

int main() {
    MyDeque<int> myDeque;

    int choice;
    int value;

    do {
        print();
        std::cout << std::endl;

        myDeque.printDeque();

        std::cout << std::endl;

        std::cout << "Select an action: ";
        std::cin >> choice;
        std::cout << "------------------------------------------------ ---------------" << std::endl;

        switch (choice) {
        case 1:
            std::cout << "Enter the value to add to the beginning: ";
            std::cin >> value;
            myDeque.addFront(value);
            break;
        case 2:
            std::cout << "Enter the value to add to the end: ";
            std::cin >> value;
            myDeque.addRear(value);
            break;
        case 3:
            myDeque.removeFront();
            break;
        case 4:
            myDeque.removeRear();
            break;
        case 5:
            std::cout << "First element: " << myDeque.peekFront() << std::endl;
            break;
        case 6:
            std::cout << "Last element: " << myDeque.peekRear() << std::endl;
            break;
        case 7:
            myDeque.swapFrontAndRear();
            break;
        case 8:
            if (myDeque.isEmpty()) {
                std::cout << "The deck is empty." << std::endl;
            }
            else {
                std::cout << "The deck is not empty." << std::endl;
            }
            break;
        case 9:
            std::cout << "Deque size: " << myDeque.getSize() << std::endl;
            break;
        case 10:
            myDeque.reverse();
            break;
        case 11:
            std::cout << "Enter value: ";
            std::cin >> value;
            if (myDeque.contains(value)) {
                std::cout << "Element " << value << " belongs to deck." << std::endl;
            }
            else {
                std::cout << "Element " << value << " does not belong to deck." << std::endl;
            }
            break;
        case 12:
            myDeque.clear();
            std::cout << "Deck cleared." << std::endl;
            break;
        case 13:
            std::cout << "Program completed." << std::endl;
            break;
        default:
            std::cout << "Invalid choice. Try again." << std::endl;
            break;
        }
        system("pause");
        system("cls");
    } while (choice != 13);
}