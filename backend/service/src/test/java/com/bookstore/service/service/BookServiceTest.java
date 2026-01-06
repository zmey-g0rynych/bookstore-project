package com.bookstore.service.service;

import com.bookstore.service.data.BookRepository;
import com.bookstore.service.model.Book;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.math.BigDecimal;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class BookServiceTest {

    @Mock
    private BookRepository bookRepository;

    @InjectMocks
    private BookService bookService;

    @Test
    void getBook_existingId_returnsBook() {
        // arrange
        Book book = new Book("Test", "John", "Doe", new BigDecimal("10.00"), "Desc", null);
        book.setId(1);

        when(bookRepository.findById(1)).thenReturn(Optional.of(book));

        // act
        Book result = bookService.getBook(1);

        // assert
        assertNotNull(result);
        assertEquals(1, result.getId());
        assertEquals("Test", result.getTitle());
        verify(bookRepository).findById(1);
    }

    @Test
    void getBook_notExistingId_throwsException() {
        // arrange
        when(bookRepository.findById(999)).thenReturn(Optional.empty());

        // act + assert
        RuntimeException ex = assertThrows(RuntimeException.class, () -> {
            bookService.getBook(999);
        });

        assertTrue(ex.getMessage().contains("Book not found"));
    }
}
