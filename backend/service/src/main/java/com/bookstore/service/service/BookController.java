package com.bookstore.service.service;

import com.bookstore.service.model.Book;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/books")
public class BookController {

    private final BookService bookService; // Измените на service

    public BookController(BookService bookService) { // Инжектим service
        this.bookService = bookService;
    }

    @GetMapping
    public List<Book> getAllBooks() {
        return bookService.getAllBooks();
    }

    @GetMapping("/{id}")
    public Book getBook(@PathVariable Integer id) {
        return bookService.getBook(id);
    }

    @GetMapping("/search")
    public List<Book> searchBooks(
            @RequestParam(required = false) String title,
            @RequestParam(required = false) String authorFirstName,
            @RequestParam(required = false) String authorLastName) {

        return bookService.searchBooks(title, authorFirstName, authorLastName);
    }
}