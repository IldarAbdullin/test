using ExempGen.CatalogObj;
using Library.CatalogObj;
using System;
using System.Collections.Generic;
using System.Collections.Generic2;

namespace ExempGen.Repository
{
    public class Repository : IRepository<ILibrObj>
    {
        private List<ILibrObj> _catalog;

        public Repository()
        {
            _catalog = new List<ILibrObj>();
        }

        //todo: нельзя просто сохранять ссылку, иначе каталог можно будет изменить извне. 
        public Repository(List<ILibrObj> list)
        {
            _catalog = list;
        }

        /// <summary>
        /// Return the catalog
        /// </summary>
        /// <returns>List of objects</returns>
        public List<ILibrObj> GetCatalog()
        {
            List<ILibrObj> catlog = new List<ILibrObj>(_catalog);
            
            return catlog;
        }

        /// <summary>
        /// View catalog
        /// </summary>
        public void ShowCatolog()
        {
            foreach (var item in _catalog)
            {
                Console.WriteLine(item.ObjToString() + "\n");
            }
        }

        /// <summary>
        /// Add item in the catalog
        /// </summary>
        /// <param name="item">item to be added to the catalog </param>
        public void AddItem(ILibrObj item)
        {
            _catalog.Add(item);
        }

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <param name="obj">Item to be removed from the catalog</param>
        public void DelateItem(ILibrObj item)
        {
            _catalog.Remove(item);
        }

        /// <summary>
        /// Search item by name
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns>List conta
        public List<ILibrObj> SearchByName(string name)
        {
            var listObj = new List<ILibrObj>();

            foreach (var item in _catalog)
            {
                //todo: используй метод Equals, иначе можешь начать сравнивать ссылки а не значения
                if(item.Name == name)
                {
                    listObj.Add(item);

                }
            }

            return listObj;
        }

        /// <summary>
        /// Sorts the list of objects in ascending order.
        /// </summary>
        /// <param name="catalog">Sortable List</param>
        /// <returns>Sorted list</returns>
        //todo: зачем ты сортируешь какой-то сторонний список?
        public List<ILibrObj> SortAscByDatePublication(List<ILibrObj> catalog)
        {
            catalog.Sort((x, y) => x.PublicationDate.CompareTo(y.PublicationDate));
            return catalog;
        }

        /// <summary>
        /// Sorts the list of objects in descending order.
        /// </summary>
        /// <param name="catalog">Sortable List</param>
        /// <returns>Sorted list</returns>
        //todo: то же самое
        public List<ILibrObj> SortDescByDatePublication(List<ILibrObj> catalog)
        {
            catalog.Sort((x, y) => x.PublicationDate.CompareTo(y.PublicationDate));
            catalog.Reverse();

            return catalog;
        }

        /// <summary>
        ///Search all author’s books
        /// </summary>
        /// <param name="author">Search author name</param>
        /// <returns>All books of the author</returns>
        public List<Book> SearchBooksByAuthor(string author)
        {
            var authorCatalog = new List<Book>();
            foreach (var item in _catalog)
            {
                Book book = item as Book;
                if (book != null)
                {
                    //todo: используй нормальный поиск либо через for/foreach проверяй каждого автора. Что если этот метод вызвать с author = ", "?
                    if(string.Join(", ", book.Authors).IndexOf(author) != -1)
                    {                        
                        authorCatalog.Add(book);
                    }
                }
            }

            return authorCatalog;
        }

        /// <summary>
        /// The output of all books whose publisher name begins with a given set of characters, with a grouping by publisher
        /// </summary>
        /// <param name="publisher">Starting character set</param>
        /// <returns>Grouped publishers matching a search criteria</returns>
        //todo: для группировки используй Dictionary
        public List<List<Book>> SearchPublisherGroupByStartWith(string publisher)
        {
            var publisherCatolog = new List<Book>();

            foreach (var item in _catalog)
            {
                Book book = item as Book;
                if (book != null)
                {
                    if (book.NamePublisher.StartsWith(publisher))
                    {
                        publisherCatolog.Add(book);
                    }
                }
            }


            var groupByNamecatalog = BookGorupByNamePublisher(publisherCatolog);

            return groupByNamecatalog;
        }

        /// <summary>
        /// Sort catalog of book bu name in ascending order
        /// </summary>
        /// <param name="catalog">Catalog to sort</param>
        /// <returns>Sorted list/returns>
        //todo: не давай доступа к методам которые не должны быть видны из вне. обычно методы объявляются после их вызова в другом методе того же класса
        public List<Book> SortBooksAscByName(List<Book> catalog)
        {
            Book[] mas = catalog.ToArray();
            Array.Sort(mas, (x, y) => x.Name.CompareTo(y.Name));
            //todo: не оставляй бесмысленные коментарии
            // List<LibrObj> newCatalog = new List<LibrObj>();
            //newCatalog.AddRange(mas);
            catalog.Clear();
            catalog.AddRange(mas);

            return catalog;
        }

        /// <summary>
        /// Sort catalog to sort of book bu name in descending order
        /// </summary>
        /// <param name="catalog">Catalog to sort</param>
        /// <returns>Sorted list/returns>
        //todo тоже самое что и выше
        public List<Book> SortBooksDescByName(List<Book> catalog)
        {
            Book[] mas = catalog.ToArray();
            Array.Sort(mas, (x, y) => x.Name.CompareTo(y.Name));
            // List<LibrObj> newCatalog = new List<LibrObj>();
            //newCatalog.AddRange(mas);
            catalog.Clear();
            catalog.AddRange(mas);
            catalog.Reverse();

            return catalog;
        }

        /// <summary>
        /// Groups catalog to sort of books by publishers
        /// </summary>
        /// <param name="bookCatalog">Catalog to sort</param>
        /// <returns></returns>
        //todo: Нельзя давайть пользователю доступ к функциям, который нет в интерфейсе и которые не должны быть ему доступны со старта. Пользователь должен иметь возможность в полной возможности использовать объект через интерфейс
        public List<List<Book>> BookGorupByNamePublisher(List<Book> bookCatalog)
        {
            var sortCatalog = SortBooksAscByName(bookCatalog);
            string namePublisher = sortCatalog[0].NamePublisher;
            List<List<Book>> listGroupByName = new List<List<Book>>();
            List<Book> currentName = new List<Book>();

            foreach (var item in sortCatalog)
            {
                if (namePublisher == item.NamePublisher)
                {
                    currentName.Add(item);
                }
                else
                {
                    namePublisher = item.NamePublisher;
                    var nowDate = new List<Book>(currentName);
                    listGroupByName.Add(nowDate);
                    currentName.Clear();
                    currentName.Add(item);
                }
            }

            if (currentName.Count != 0)
            {
                var nowName = new List<Book>(currentName);
                listGroupByName.Add(nowName);
                currentName.Clear();
            }

            return listGroupByName;
        }

        /// <summary>
        /// Grouping the internal catalog by publisher
        /// </summary>
        /// <returns>Grouped publishers</returns>
        //todo: название метода не отображает его действия, так что в суммари ничего не сказано, что группировка идет именно по году публикации
        public List<List<ILibrObj>> GroupByPublicationDate()
        {
            _catalog.Sort((x, y) => x.PublicationDate.CompareTo(y.PublicationDate));
            DateTime date = _catalog[0].PublicationDate;
            List<List<ILibrObj>> listGroupByDate = new List<List<ILibrObj>>();
            List<ILibrObj> currentDate = new List<ILibrObj>();

            foreach (var item in _catalog)
            {
                if (date.Year == item.PublicationDate.Year)
                {
                    currentDate.Add(item);
                }
                else
                {
                    date = item.PublicationDate;
                    var nowDate = new List<ILibrObj>(currentDate);
                    listGroupByDate.Add(nowDate);
                    currentDate.Clear();
                    currentDate.Add(item);
                }
            }

            if (currentDate.Count != 0)
            {
                var nowDate = new List<ILibrObj>(currentDate);
                listGroupByDate.Add(nowDate);
                currentDate.Clear();
            }

            return listGroupByDate;
        }

        //todo: выглядит как что-то лишнее в данном задании. Убери
        /// <summary>
        /// first find after sort
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="pred"></param>
        /// <param name="compr"></param>
        public void SortOrSearchByParam(List<ILibrObj> inputList, Predicate<ILibrObj> pred, Comparison<ILibrObj> compr)
        {
            var arr = inputList.ToArray();

            if (pred != null) 
            {
                Array.Find(arr, pred);
            }

            if (compr != null)
            {
                Array.Sort(arr, compr);
            }       
        }

        //todo не оставляй мусора в проекте. Удали все лишнее
        public bool Validate(ILibrObj obj)
        {
            //SortOrSearchByParam(_catalog, x => x is Book, (x, y) =>
            //{
            //    if (x.PublicationDate > y.PublicationDate)
            //    {
            //        return 1;
            //    }
            //    else if (x.PublicationDate == y.PublicationDate)
            //    {
            //        return 0;
            //    }
            //    else
            //    {
            //        return 1;
            //    }
            //};

            if ((obj.Name == string.Empty) || (obj.AmountPages < 1) || (obj.Note.Length > 500) || (obj.Price < 1m))
            {
                return false;
            }

            if(obj is Book)
            {
                var book = (Book)obj;
                
                if((book.Authors.Count < 1) || (book.CityPublic == string.Empty) || (book.NamePublisher == string.Empty) || (book.AmountCopies < 0) || (book.PublicationDate < new DateTime(1900, 1, 1)))
                {
                    return false;
                }
            }
            else if (obj is Newspaper)
            {
                var np = (Newspaper)obj;

                if ((np.CityPublic == string.Empty) || (np.NamePublisher == string.Empty) || (np.Number < 0) || (np.Date != null) || (np.PublicationDate < new DateTime(1900, 1, 1)))
                {
                    return false;
                }
            }
            else if(obj is Patent)
            {
                var pat = (Patent)obj;

                if ((pat.Inventors.Count == 0) || (pat.Country == string.Empty) || (pat.ApplicationDate < new DateTime(1900, 1, 1)) || (pat.PublicationDate < new DateTime(1950, 1, 1)))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        
        public void WriteLog(string info)
        {

        }
        //public void InsertAndLog(ILibrObj obj)
        //{
        //    string info = ObjToString(obj) + "\n";
        //    bool flag = true;
        //    ValidateLibrObjAndLog(obj, ref info, ref flag);

        //    if (obj is Book)
        //    {
        //        var book = (Book)obj;
        //        ValidateBookAndLog(book, ref info, ref flag);
        //    }
        //    else if (obj is Newspaper)
        //    {
        //        var np = (Newspaper)obj;
        //        ValidateNewsPaperAndLog(np, ref info, ref flag);
        //    }
        //    else if (obj is Patent)
        //    {
        //        var pat = (Patent)obj;
        //        ValidatePatentAndLog(pat, ref info, ref flag);
        //    }

        //    if(!flag)
        //    {
        //        WriteLog(info);
        //    }

        //    AddItem(obj);
        //}

        public void ValidateLibrObjAndLog(ILibrObj obj, ref string info, ref bool flag)
        {
            if (obj.Name == string.Empty)
            {
                info += "Name is empty \n";
                flag = false;
            }

            if (obj.AmountPages < 1)
            {
                info += "Amount Pages < 1 \n";
                flag = false;
            }

            if (obj.Note.Length > 500)
            {
                info += "Note > 500 \n";
                flag = false;
            }

            if (obj.Price < 1m)
            {
                info += "Price < 1m \n";
                flag = false;
            }
        }

        public void ValidateBookAndLog(Book book, ref string info, ref bool flag)
        {
            if (book.Authors.Count < 1)
            {
                flag = false;
                info += "Amount authors < 1 \n";
            }

            if (book.CityPublic == string.Empty)
            {
                flag = false;
                info += "Amount authors < 1 \n";
            }

            if (book.NamePublisher == string.Empty)
            {
                flag = false;
                info += "Name Publisher is empty \n";
            }

            if (book.AmountCopies < 0)
            {
                flag = false;
                info += "Amount copies < 0 \n";
            }

            if (book.PublicationDate < new DateTime(1900, 1, 1))
            {
                flag = false;
                info += "Date of publication older than 1900 \n";
            }
        }

        public void ValidateNewsPaperAndLog(Newspaper np, ref string info, ref bool flag)
        {
            if (np.CityPublic == string.Empty)
            {
                flag = false;
                info += "City of pablication is empty \n";
            }

            if (np.NamePublisher == string.Empty)
            {
                flag = false;
                info += "Name Publisher is empty \n";
            }

            if (np.Number <= 0)
            {
                flag = false;
                info += "number <= \n";
            }

            if (np.Date != null)
            {
                flag = false;
                info += "Date is null \n";
            }

            if (np.PublicationDate < new DateTime(1900, 1, 1))
            {
                info += "Date of publication older than 1900 \n";
            }
        }

        public void ValidatePatentAndLog(Patent pat, ref string info, ref bool flag)
        {
            if (pat.Inventors.Count == 0) 
            {
                flag = false;
                info += "Amount Invetors = 0 \n";
            }

            if (pat.Country == string.Empty)
            {
                flag = false;
                info += "Country is empty \n";
            }

            if (pat.ApplicationDate < new DateTime(1900, 1, 1))
            {
                flag = false;
                info += "Date of application older than 1900 \n";
            }

            if (pat.PublicationDate < new DateTime(1950, 1, 1))
            {
                flag = false;
                info += "Date of publication older than 1900 \n";
            }
        }
    }
}
