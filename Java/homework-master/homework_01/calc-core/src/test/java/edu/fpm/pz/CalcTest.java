package edu.fpm.pz;

import org.junit.Assert;
import org.junit.Test;

public class CalcTest {
    private Calc calc = new CalcImpl();

    @Test
    public void addition() {
        double num1 = 10;
        double num2 = 20;
        double expected = 30;

        double result = calc.addition(num1, num2);

        Assert.assertEquals(expected, result, 0);
    }

    @Test
    public void subtraction() {
        double num1 = 10;
        double num2 = 20;
        double expected = -10;

        double result = calc.subtraction(num1, num2);

        Assert.assertEquals(expected, result, 0);
    }

    @Test
    public void multiplication() {
        double num1 = 10;
        double num2 = 20;
        double expected = 200;

        double result = calc.multiplication(num1, num2);

        Assert.assertEquals(expected, result, 0);
    }

    @Test
    public void division() {
        double num1 = 10;
        double num2 = 20;
        double expected = 0.5;

        double result = calc.division(num1, num2);

        Assert.assertEquals(expected, result, 0);
    }

    @Test
    public void task() {
        double acceleration = 1.5;
        double expected = 6.32;

        double result = Math.round(calc.task(acceleration) * 100.0) / 100.0;

        Assert.assertEquals(expected, result, 0);
    }
}
