package com.bookstore.service.service;

import com.bookstore.service.model.Book;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.web.servlet.MockMvc;

import java.math.BigDecimal;
import java.util.List;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@WebMvcTest(BookController.class)
class BookControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private BookService bookService;

    @Test
    void getAllBooks_returns200AndJson() throws Exception {
        var book = new Book("Test", "John", "Doe", new BigDecimal("100"), "Desc", null);
        Mockito.when(bookService.getAllBooks()).thenReturn(List.of(book));

        mockMvc.perform(get("/api/books"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$[0].title").value("Test"))
                .andExpect(jsonPath("$[0].authorFirstName").value("John"));
    }

    @Test
    void getBookById_returnsBook() throws Exception {
        var book = new Book("Test", "John", "Doe", new BigDecimal("100"), "Desc", null);
        Mockito.when(bookService.getBook(1)).thenReturn(book);

        mockMvc.perform(get("/api/books/1"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.title").value("Test"));
    }
}
