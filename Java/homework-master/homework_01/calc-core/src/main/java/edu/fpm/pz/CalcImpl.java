package edu.fpm.pz;

import java.util.Scanner;

/**
 * This is implementation of Calc
 */
public class CalcImpl implements Calc {
    public double addition(double num1, double num2) {
        return num1 + num2;
    }

    public double subtraction(double num1, double num2) {
        return num1 - num2;
    }

    public double multiplication(double num1, double num2) {
        return num1 * num2;
    }

    public double division(double num1, double num2) {
        double tmp = 0;
        try {
            tmp = num1 / num2;
        } catch (ArithmeticException e) {
            System.out.println("Division by zero");
            tmp = 0;
        }
        return tmp;
    }

    public double task(double acceleration) {
        int distance = 30;
        double time = Math.sqrt(2 * distance / acceleration);

        System.out.println("Time: " + time);

        if (time > 15) {
            System.out.println("Increase your acceleration!");
        } else if (time >= 5 && time <= 15) {
            System.out.println("Acceleration is normal");
        } else {
            System.out.println("Reduce your acceleration!");
        }

        return time;
    }
}
