package edu.fpm.pz;

import java.util.Scanner;

/**
 * My Demo
 */
public class App {
    public static void main(String[] args) {
        if (args.length < 3) {
            System.err.println("Not enought parameters!");
            return;
        }
        Calc calc = new CalcImpl();
        double num1;
        try {
            num1 = Double.parseDouble(args[0]);
        } catch (Exception e) {
            System.err.println("Invalid first argument!");
            return;
        }
        double num2;
        try {
            num2 = Double.parseDouble(args[1]);
        } catch (Exception e) {
            System.err.println("Invalid second argument!");
            return;
        }
        double result = 0;
        switch (args[2]) {
            case "+":
                result = calc.addition(num1, num2);
                break;
            case "-":
                result = calc.subtraction(num1, num2);
                break;
            case "*":
                result = calc.multiplication(num1, num2);
                break;
            case "/":
                result = calc.division(num1, num2);
                break;
            default: {
                System.err.println("Invalid operator!");
                return;
            }
        }
        System.out.println("number1= " + num1 + " number2= " + num2 + " operator= " + args[2] + " result= " + result);

        Scanner scanner = new Scanner(System.in);
        System.out.println("Enter the acceleration: ");
        double acceleration = scanner.nextDouble();
        calc.task(acceleration);
    }
}
