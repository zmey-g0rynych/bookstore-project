package com.bookstore.service.data;

import com.bookstore.service.model.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;

public interface BookRepository extends JpaRepository<Book, Integer> {

    List<Book> findByTitleContains(String value);

    List<Book> findByAuthorFirstNameContains(String firstName);

    List<Book> findByAuthorLastNameContains(String lastName);
}
