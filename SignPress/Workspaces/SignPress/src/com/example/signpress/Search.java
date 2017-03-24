package com.example.signpress;

import java.io.Serializable;

public class Search implements Serializable{
	private int Year;
	private int CategoryId;
	public int getYear() {
		return Year;
	}
	public void setYear(int year) {
		Year = year;
	}
	public int getCategoryId() {
		return CategoryId;
	}
	public void setCategoryId(int categoryId) {
		CategoryId = categoryId;
	}
}
