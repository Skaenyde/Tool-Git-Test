using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Common.Models
{
    [Serializable()]
    public class DataTableResponse<T>
    {
        public DataTableResponse(T value)
        {
            data = value;
        }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T data { get; set; }
        public string order { get; set; }

        public DataTableResponse() { }
    }

    [Serializable()]
    public class DataTableParameters<T>
    {
        public DataTableParameters(T value)
        {
            query = value;
        }
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public List<columm> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }

        public T query { get; set; }

        public Pagination pagination { get; set; }
        public DataTableParameters() { }
    }

    [Serializable()]
    public class Query
    {
        public string LanguageName { get; set; }
    }
    [Serializable()]
    public class Pagination
    {
        public string page { get; set; }
        public string pages { get; set; }
    }
    [Serializable()]
    public class columm
    {
        public string data { get; set; }
        public string name { get; set; }
        public Boolean searchable { get; set; }
        public Boolean orderable { get; set; }
        public Search search { get; set; }
    }
    [Serializable()]
    public class Search
    {
        public string value { get; set; }
        public Boolean regex { get; set; }
    }

    [Serializable()]
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

}
