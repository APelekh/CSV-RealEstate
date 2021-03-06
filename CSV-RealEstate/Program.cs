﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_RealEstate
{
    // WHERE TO START?
    // 1. Complete the RealEstateType enumeration
    // 2. Complete the RealEstateSale object.  Fill in all properties, then create the constructor.
    // 3. Complete the GetRealEstateSaleList() function.  This is the function that actually reads in the .csv document and extracts a single row from the document and passes it into the RealEstateSale constructor to create a list of RealEstateSale Objects.
    // 4. Start by displaying the the information in the Main() function by creating lambda expressions.  After you have acheived your desired output, then translate your logic into the function for testing.
    class Program
    {
        
        static void Main(string[] args)
        {
            
            List<RealEstateSale> realEstateSaleList = GetRealEstateSaleList();
            //Display the average square footage of a Condo sold in the city of Sacramento, 
            double test = realEstateSaleList.Where(x => x.City == "SACRAMENTO").Where(x => x.RealEstateType == RealEstateType.Condo).Average(x => x.SquareFeet);
            Console.WriteLine(test);
            //Use the GetAverageSquareFootageByRealEstateTypeAndCity() function.

            //Display the total sales of all residential homes in Elk Grove.  Use the GetTotalSalesByRealEstateTypeAndCity() function for testing.
            int totalSales = realEstateSaleList.Where(x => x.City == "ELK GROVE").Where(x => x.RealEstateType == RealEstateType.Residential).Sum(x => x.Price);
            Console.WriteLine(Convert.ToDecimal(totalSales));
            decimal xxx = 10m;
            Console.WriteLine(Convert.ToDecimal(xxx/3));

            //Display the total number of residential homes sold in the zip code 95842.  Use the GetNumberOfSalesByRealEstateTypeAndZip() function for testing.
            int totalNumberOfHomes = realEstateSaleList.Where(x => x.ZipCode == 95842).Where(x => x.RealEstateType == RealEstateType.Residential).Count();
            Console.WriteLine(totalNumberOfHomes);

            //Display the average sale price of a lot in Sacramento.  Use the GetAverageSalePriceByRealEstateTypeAndCity() function for testing.
            double averagePrice = realEstateSaleList.Where(x => x.City == "SACRAMENTO").Where(x => x.RealEstateType == RealEstateType.Lot).Average(x => x.Price);
            Console.WriteLine(averagePrice);
            
            //Display the average price per square foot for a condo in Sacramento. Round to 2 decimal places. Use the GetAveragePricePerSquareFootByRealEstateTypeAndCity() function for testing.
            double averagePricePerFoot = realEstateSaleList.Where(x => x.City == "SACRAMENTO").Where(x => x.RealEstateType == RealEstateType.Condo).Average(x => x.Price / x.SquareFeet);
            Console.WriteLine("\n" + Math.Round(Convert.ToDecimal(averagePricePerFoot), 2));

            //Display the number of all sales that were completed on a Wednesday.  Use the GetNumberOfSalesByDayOfWeek() function for testing.
            int numberOfSales = realEstateSaleList.Where(x => x.SaleDate.DayOfWeek == DayOfWeek.Wednesday).Select(x => x.Price).Count();
            Console.WriteLine(numberOfSales);


            //Display the average number of bedrooms for a residential home in Sacramento when the 
            // price is greater than 300000.  Round to 2 decimal places.  Use the GetAverageBedsByRealEstateTypeAndCityHigherThanPrice() function for testing.
            double numberOfBedrooms = realEstateSaleList.Where(x => x.City == "SACRAMENTO" && x.RealEstateType == RealEstateType.Residential && x.Price > 30000).Average(x => x.Beds);
            Console.WriteLine(numberOfBedrooms);

            GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "SACRAMENTO");

            //Extra Credit:
            //Display top 5 cities by the number of homes sold (using the GroupBy extension)
            // Use the GetTop5CitiesByNumberOfHomesSold() function for testing.
            List<IGrouping<string, RealEstateSale>> myList = realEstateSaleList.GroupBy(x => x.City).OrderByDescending(x => x.Sum(y => y.Price)).ToList();
            List<IGrouping<string, RealEstateSale>> holdList = realEstateSaleList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).ToList();
            List<string> returnList = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                returnList.Add(holdList.ElementAt(i).Key);
            }
            

            Console.ReadKey();
        }
        /// <summary>
        /// Processes an csv file and creates a list of RealEstate objects
        /// </summary>
        /// <returns>Returns a list of objects</returns>
        public static List<RealEstateSale> GetRealEstateSaleList()
        {
         
            //read in the realestatedata.csv file.  As you process each row, you'll add a new 
            // RealEstateData object to the list for each row of the document, excluding the first.  bool skipFirstLine = true;
            //declearing new streamreader
            StreamReader reader = new StreamReader("realestatedata.csv");
            //skipping first line
            string firstline = reader.ReadLine();
            //creating a list of objects
            List<RealEstateSale> list = new List<RealEstateSale>();
            //reading csv file and adding each line to my list
            while (!reader.EndOfStream)
            {
                list.Add(new RealEstateSale(reader.ReadLine()));
            }

            return list;
        }

        public static double GetAverageSquareFootageByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city) 
        {
            return realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Average(x => x.SquareFeet); 
        }

        public static decimal GetTotalSalesByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            return Convert.ToDecimal(realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Sum(x => x.Price));
        }

        public static int GetNumberOfSalesByRealEstateTypeAndZip(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string zipcode)
        {
            return realEstateDataList.Where(x => x.ZipCode == int.Parse(zipcode)).Where(x => x.RealEstateType == realEstateType).Count(); ;
        }

        
        public static decimal GetAverageSalePriceByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            //double holderOne = realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Average(x => x.Price);
            //decimal holderTwo = Convert.ToDecimal(holderOne);
            //return Math.Round(holderTwo, 2);
            return Math.Round(Convert.ToDecimal(realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Average(x => x.Price)), 2);
        }
        public static decimal GetAveragePricePerSquareFootByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            //double holder = realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Sum(x => x.Price);
            //double holderTwo = realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Sum(x => x.SquareFeet);
            //double result = holder / holderTwo;
            //return Math.Round(Convert.ToDecimal(result), 2);
            return Math.Round(Convert.ToDecimal(realEstateDataList.Where(x => x.City == city.ToUpper()).Where(x => x.RealEstateType == realEstateType).Average(x => x.Price / x.SquareFeet)), 2);
        }

        public static int GetNumberOfSalesByDayOfWeek(List<RealEstateSale> realEstateDataList, DayOfWeek dayOfWeek)
        {
            return realEstateDataList.Where(x => x.SaleDate.DayOfWeek == dayOfWeek).Select(x => x.Price).Count();
        }

        public static double GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city, decimal price)
        {
            //Must round to 2 decimal points
            double numberOfBedrooms = realEstateDataList.Where(x => x.City == city.ToUpper() && x.RealEstateType == realEstateType && x.Price > price).Average(x => x.Beds);
            return Math.Round(numberOfBedrooms, 2);
        }

        public static List<string> GetTop5CitiesByNumberOfHomesSold(List<RealEstateSale> realEstateDataList)
        {
            //grouping by cities and ordering by the number of sales
            List<IGrouping<string, RealEstateSale>> holdList = realEstateDataList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).ToList();
            //creating a list of string to store the names of cities
            List<string> returnList = new List<string>();
            //adding cities names to my return list
            for (int i = 0; i < 5; i++)
            {
                returnList.Add(holdList.ElementAt(i).Key);
            }
            return returnList;
            //OR
            //return realEstateDataList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).Select(x => x.Key).Take(5).ToList();
            return realEstateDataList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).Select(x => x.Select(y => y.City).Take(5).ToString().ToList());
        }
    }
    /// <summary>
    /// Enumerable for Real estate type
    /// </summary>
    public enum RealEstateType
    {
        //fill in with enum types: Residential, MultiFamily, Condo, Lot
        Residential,
        MultiFamily,
        Condo,
        Lot
    }
    class RealEstateSale
    {
        //Create properties, using the correct data types (not all are strings) for all columns of the CSV
        
        //creating properties
        private string _street;
        public string Street
        {
            get { return _street.ToUpper(); }
            set { _street = value; }
        }

        private string _city;
        public string City
        {
            get { return _city.ToUpper(); }
            set { _city = value; }
        }

        private int _zip;
        public int ZipCode
        {
            get { return _zip; }
            set { _zip = value; }
        }
        private string _state;
        public string State
        {
            get { return _state.ToUpper(); }
            set { _state = value;  }
        }
        private int _beds;
        public int Beds
        {
            get { return _beds; }
            set { _beds = value; }
        }
        private int _baths;
        public int Baths
        {
            get { return _baths; }
            set { _baths = value; }
        }
        private double _squareFeet;
        public double SquareFeet
        {
            get { return _squareFeet; }
            set { _squareFeet = value; }
        }

        private RealEstateType _realEstateType;
        public RealEstateType RealEstateType
        {
            get { return _realEstateType; }
            set { _realEstateType = value; }
        }

        private DateTime _saleDate;
        public DateTime SaleDate
        {
            get { return _saleDate; }
            set { _saleDate = value; }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        //The constructor will take a single string arguement.  This string will be one line of the real estate data.
        // Inside the constructor, you will seperate the values into their corrosponding properties, and do the necessary conversions
        /// <summary>
        /// Constructor that takes a string, breaks it down by comma and initializes properties
        /// </summary>
        /// <param name="inputLine"></param>
        public RealEstateSale(string inputLine)
        {
            string[] realEstateList = inputLine.Split(',');

            this.Street = realEstateList[0];
            this.City = realEstateList[1];
            this.ZipCode = int.Parse(realEstateList[2]);
            this.State = realEstateList[3];
            this.Beds = int.Parse(realEstateList[4]);
            this.Baths = int.Parse(realEstateList[5]);
            this.SquareFeet = Convert.ToDouble(realEstateList[6]);
            //here I'm checking the input values for setting correct enumerable values
            if (int.Parse(realEstateList[6]) == 0)
            {
                this.RealEstateType = RealEstateType.Lot;
            }
            else if (realEstateList[7] == "Residential")
            {
                this.RealEstateType = RealEstateType.Residential;
            }
            else if (realEstateList[7] == "Multi-Family")
            {
                this.RealEstateType = RealEstateType.MultiFamily;
            }
            else if (realEstateList[7] == "Condo")
            {
                this.RealEstateType = RealEstateType.Condo;
            }
            this.SaleDate = Convert.ToDateTime(realEstateList[8]);
            this.Price = int.Parse(realEstateList[9]);

        }
        //When computing the RealEstateType, if the square footage is 0, then it is of the Lot type, otherwise, use the string
        // value of the "Type" column to determine its corresponding enumeration type.
    }
}
