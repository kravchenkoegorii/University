#include <iostream>
#include "multilist.cpp"

void print() {
    std::cout << "1. Add" << std::endl;
    std::cout << "2. Remove level with sublevels" << std::endl;
    std::cout << "3. Remove branch" << std::endl;
    std::cout << "4. Create copy" << std::endl;
    std::cout << "5. Clear multilist" << std::endl;
    std::cout << "6. Exit the program" << std::endl;
}

int main() {
    Multilist<int> myList;
    Multilist<int> copy;

    int choice;
    int value;
    int search_value;
    string neighbour;

    do {
        print();
        std::cout << std::endl;

        myList.print();

        std::cout << std::endl;

        std::cout << "Select an action: ";
        std::cin >> choice;
        std::cout << "---------------------------------------------------------------" << std::endl;

        switch (choice) {
        case 1:
            std::cout << "Enter the value to add: ";
            std::cin >> value;
            std::cout << "Enter the value to connect with: ";
            std::cin >> search_value;
            std::cout << "Enter how connect: c - child, n - neighbour: ";
            std::cin >> neighbour;
            myList.add(value, search_value, neighbour);
            break;
        case 2:
            std::cout << "Enter the level to delete: ";
            std::cin >> value;
            myList.clear_level(value);
            break;
        case 3:
            std::cout << "Enter the value to find and delete branch: ";
            std::cin >> value;
            myList.clear_branch_by_value(value);
            break;
        case 4:
            std::cout << "COPY:" << std::endl;
            copy = myList.copy();
            copy.print();
            break;
        case 5:
            myList.clear();
            break;
        case 6:
            std::cout << "Exit the program." << std::endl;
            break;
        default:
            std::cout << "Invalid choice. Try again." << std::endl;
            break;
        }
        system("pause");
        system("cls");
    } while (choice != 6);
}