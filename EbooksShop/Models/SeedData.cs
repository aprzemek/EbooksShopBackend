using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EbooksShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EbooksShopContext(serviceProvider.GetRequiredService<DbContextOptions<EbooksShopContext>>()))
            {
                if (context.Pages.Any())
                {
                    return;
                }

                context.Pages.AddRange(
                    new Page
                    {
                        Name = "Strona Główna",
                        Slug = "main_page",
                        Content = "Strona Główna"
                    },
                    new Page
                    {
                        Name = "O nas",
                        Slug = "about_us",
                        Content = "Strona o nas"
                    },
                    new Page
                    {
                        Name = "Usługi",
                        Slug = "services",
                        Content = "Strona o naszych usługach"
                    },
                    new Page
                    {
                        Name = "Kontakt",
                        Slug = "contact",
                        Content = "Stron z kontaktem"
                    }
                );
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Frond-end",
                        Slug = "front_end"
                    },
                    new Category
                    {
                        Name = "Back-end",
                        Slug = "back_end"
                    },
                    new Category
                    {
                        Name = "Testowanie",
                        Slug = "testing"
                    }
                );
                context.Products.AddRange(
                    new Product
                    {
                        Name = "C# 8.0 w pigułce",
                        CategoryId = 2,
                        Description = "Sprawdź, jak w C# pracują najlepsi programiści!",
                        Image = "c8wpig.jpg",
                        Price = 89.40M
                    },
                    new Product
                    {
                        Name = "Java. Podstawy. Wydanie XI",
                        CategoryId = 2,
                        Description = "Java - oto język mistrzów programowania!",
                        Image = "javp11.jpg",
                        Price = 59.40M
                    },
                    new Product
                    {
                        Name = "JavaScript. Przewodnik. Poznaj język mistrzów programowania. Wydanie VII",
                        CategoryId = 1,
                        Description = "Dowiedz się wszystkiego, co musisz wiedzieć o JavaScripcie!",
                        Image = "jsppm7.jpg",
                        Price = 71.40M
                    },
                    new Product
                    {
                        Name = "Angular. Profesjonalne techniki programowania. Wydanie IV",
                        CategoryId = 1,
                        Description = "Dobre rozwiązanie dla aplikacji klienta? Z Angularem się uda!",
                        Image = "angup4.jpg",
                        Price = 89.40M
                    },
                    new Product
                    {
                        Name = "Python na start!",
                        CategoryId = 2,
                        Description = "Zaklinaj węża, czyli programuj w Pythonie!",
                        Image = "zaprpy.jpg",
                        Price = 13.50M
                    },
                    new Product
                    {
                        Name = "C#. Praktyczny kurs. Wydanie III",
                        CategoryId = 2,
                        Description = "Stwórz własną aplikację w języku C#!",
                        Image = "cshpk3.jpg",
                        Price = 33.40M
                    },
                    new Product
                    {
                        Name = "Vue.js 2. Wprowadzenie dla profesjonalistów",
                        CategoryId = 1,
                        Description = "Vue.js 2: zyskaj większe możliwości i pisz najlepsze aplikacje!",
                        Image = "vue2wp.jpg",
                        Price = 59.40M
                    },
                    new Product
                    {
                        Name = "Laravel. Wstęp do programowania aplikacji internetowych",
                        CategoryId = 1,
                        Description = "Twórz nowoczesne aplikacje przy użyciu doskonałego frameworka!",
                        Image = "larwpa.jpg",
                        Price = 29.40M
                    },
                    new Product
                    {
                        Name = "Android Studio. Tworzenie aplikacji mobilnych",
                        CategoryId = 2,
                        Description = "Aplikacje do zadań specjalnych... pisz tylko w Android Studio!",
                        Image = "anstam.jpg",
                        Price = 41.40M
                    },
                    new Product
                    {
                        Name = "PHP 7. Algorytmy i struktury danych",
                        CategoryId = 2,
                        Description = "Algorytmy: poznaj, zrozum, stosuj!",
                        Image = "php7al.jpg",
                        Price = 35.90M
                    },
                    new Product
                    {
                        Name = "HTML5. Tworzenie gier z wykorzystaniem CSS i JavaScript",
                        CategoryId = 1,
                        Description = "Spróbuj swoich sił i zrealizuj pomysły na własną grę!",
                        Image = "htcsjs.jpg",
                        Price = 3M
                    },
                    new Product
                    {
                        Name = "Vue.js w akcji",
                        CategoryId = 1,
                        Description = "Vue.js. Napisz piękną aplikację!",
                        Image = "vueakc.jpg",
                        Price = 35.40M
                    },
                    new Product
                    {
                        Name = "Vue.js w akcji",
                        CategoryId = 3,
                        Description = "Vue.js. Napisz piękną aplikację!",
                        Image = "vueakc.jpg",
                        Price = 35.40M
                    }
               );

                context.SaveChanges();
            }
        }
    }
}
