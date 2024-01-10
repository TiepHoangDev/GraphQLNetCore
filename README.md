# GraphQLNetCore
### https://chillicream.com/docs/hotchocolate/v13/defining-a-schema

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

> ### FILTER
```js

mutation add {
  add1: addBook(book: { id: "1", idAuthor: "1", author: { id: "1" } }) {
    id
  }
  add2: addBook(book: { id: "12", idAuthor: "1"}) {
    id
  }
  add3: addBook(book: { id: "2", idAuthor: "1"}) {
    id
  }
  add4: addBook(book: { id: "22", idAuthor: "1"}) {
    id
  }
}

query filter {
  efCoreGetBooks(first: 5, where: { or: [{ id: { startsWith: "1" } }] }) {
    nodes {
      id
      title
      author {
        id
        name
      }
    }
  }
}


```

> ### Sorting
```js
query order {
  efCoreGetBooks(first: 5, order: { id: DESC, title: ASC }) {
    nodes {
      id
      title
      author {
        id
        name
      }
    }
  }
}
```

> ### DataLoader
```js
mutation add {
  add1: addBook(book: { id: "1", idAuthor: "1", author: { id: "1" } }) {
    id
  }
  add2: addBook(book: { id: "12", idAuthor: "1" }) {
    id
  }
  add3: addBook(book: { id: "2", idAuthor: "1" }) {
    id
  }
  add4: addBook(book: { id: "22", idAuthor: "1" }) {
    id
  }
}

query filter {
  a: bookDataLoader(id: "1"){
    id
  }
  b: bookDataLoader(id: "2"){
    id
  }
}

```

> ### DataLoader from delegate
```js
mutation add {
  add1: addBook(book: { id: "1", idAuthor: "1", author: { id: "1" } }) {
    id
  }
  add2: addBook(book: { id: "12", idAuthor: "1" }) {
    id
  }
  add3: addBook(book: { id: "2", idAuthor: "1" }) {
    id
  }
  add4: addBook(book: { id: "22", idAuthor: "1" }) {
    id
  }
}

query filter {
  a: bookDataLoaderDelegate(id: "1"){
    id
  }
  b: bookDataLoaderDelegate(id: "2"){
    id
  }
}

```
