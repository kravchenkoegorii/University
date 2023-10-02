package edu.fpm.pz;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.mockito.ArgumentCaptor;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertThrows;
import static org.mockito.Mockito.*;

public class ProcessorTest {
    @Mock
    private Producer producerMock;

    @Mock
    private Consumer consumerMock;

    @InjectMocks
    private Processor processor;

    @Before
    public void setUp() {
        MockitoAnnotations.initMocks(this);
    }

    @Test
    public void testProcessWithNonNullValue() {
        String expectedValue = "Magic value";
        when(producerMock.produce()).thenReturn(expectedValue);

        processor.process();

        verify(producerMock).produce();
        ArgumentCaptor<String> argumentCaptor = ArgumentCaptor.forClass(String.class);
        verify(consumerMock).consume(argumentCaptor.capture());
        assertEquals(expectedValue, argumentCaptor.getValue());
    }

    @Test
    public void testProcessWithNullValue() {
        when(producerMock.produce()).thenReturn(null);

        assertThrows(IllegalStateException.class, processor::process);
    }

    @Test
    public void testProcessWithNullValueTryCatch() {
        when(producerMock.produce()).thenReturn(null);

        try {
            processor.process();
            Assert.fail();
        } catch (IllegalStateException e) {
            verify(producerMock, times(1)).produce();
            verify(consumerMock, never()).consume("Magic value");
        }
    }
}