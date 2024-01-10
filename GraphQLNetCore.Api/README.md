# GraphQLNetCore

```js
#get by ids
query getbyids{
  books(ids: []) {
    id
    title
    author {
      name
    }
  }
}

#create
mutation create{
  addBook(book: { id: "3", title: "book 3", author: { name: "author 3" } }) {
    id
  }
}

#update
mutation update{
  updateBook(
    id: "2"
    book: { id: "2", title: "updated 2", author: { name: "author 2" } }
  ) {
    id
  }
}

#delete
mutation delete{
  deleteBook(id: "2")
}


```

> ### subscription

```js
subscription{
  bookAdded{
    id,
    title,
    author{
      name
    }
  }
}
```

> ### Paging
```js
query paging{
  bookPaging(first: 3, after: "5", last: 1, before: "901") {
      edges {
        cursor
        node {
          id
          title
          author{
            name
          }
        }
      }
    }
}
```