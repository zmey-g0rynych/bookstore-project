package com.bookstore.service.service;

import com.bookstore.service.data.BookRepository;
import com.bookstore.service.model.Book;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/books")
public class BookController {

    private final BookRepository bookRepository;

    public BookController(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    @GetMapping
    public List<Book> getAllBooks() {
        return bookRepository.findAll();
    }

    @GetMapping("/{id}")
    public Book getBook(@PathVariable Integer id) {
        return bookRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Book not found with id " + id));
    }

    @GetMapping("/search")
    public List<Book> searchBooks(
            @RequestParam(required = false) String title,
            @RequestParam(required = false) String authorFirstName,
            @RequestParam(required = false) String authorLastName,
            @RequestParam(required = false) String description) {

        if (title != null && !title.isEmpty()) {
            return bookRepository.findByTitleContains(title);
        }
        if (authorFirstName != null && !authorFirstName.isEmpty()) {
            return bookRepository.findByAuthorFirstNameContains(authorFirstName);
        }
        if (authorLastName != null && !authorLastName.isEmpty()) {
            return bookRepository.findByAuthorLastNameContains(authorLastName);
        }

        return bookRepository.findAll();
    }
}
