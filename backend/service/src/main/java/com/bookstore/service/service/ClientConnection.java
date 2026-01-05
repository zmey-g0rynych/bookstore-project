package com.bookstore.service.service;

import jakarta.servlet.*;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.stereotype.Component;

import java.io.IOException;

@Component
public class ClientConnection implements Filter {

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {

        HttpServletRequest httpRequest = (HttpServletRequest) request;
        String clientIp = request.getRemoteAddr();
        String path = httpRequest.getRequestURI();

        System.out.println("Client with IP " + clientIp + " connected to " + path);

        try {
            chain.doFilter(request, response);
        } finally {
            System.out.println("Client with IP " + clientIp + " disconnected from "  + path);
        }
    }
}
