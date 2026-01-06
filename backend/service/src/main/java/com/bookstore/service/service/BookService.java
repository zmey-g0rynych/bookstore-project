package com.bookstore.service.service;

import com.bookstore.service.data.BookRepository;
import com.bookstore.service.model.Book;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class BookService {

    private final BookRepository bookRepository;

    @Autowired
    public BookService(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    public List<Book> getAllBooks() {
        // Можно добавить бизнес-логику здесь позже
        return bookRepository.findAll();
    }

    public Book getBook(Integer id) {
        return bookRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Book not found with id " + id));
    }

    public List<Book> searchBooks(String title, String authorFirstName, String authorLastName) {
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