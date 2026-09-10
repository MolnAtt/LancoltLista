using System;
using System.Collections.Generic;
using System.CodeDom;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace LancoltLista
{
	internal class Program
	{

		

		class LancoltLista<T>
		{
			class Elem<T>
			{
				public Elem<T> bal; // rekurzív adatszerkezet!
				public T ertek;
				public Elem<T> jobb;

				public Elem(Elem<T> bal, T ertek, Elem<T> jobb)
				{
					this.bal = bal;
					this.ertek = ertek;
					this.jobb = jobb;
				}

				public Elem() // ez hoz létre fejelemet
				{
					this.bal = this;
					this.ertek = default;
					this.jobb = this;
				}

				public void Beszúr(T ertek)
				{
					Elem<T> uj = new Elem<T>(this, ertek, this.jobb);
					this.jobb.bal = uj;
					this.jobb = uj;
				}

				public void Remove()
				{
					this.bal.jobb = this.jobb;
					this.jobb.bal = this.bal;
				}
			}

			private Elem<T> fejelem;
			private int count;

			public LancoltLista()
			{
				fejelem = new Elem<T>();
				count = 0;
			}

			public void Add(T ertek)
			{
				fejelem.bal.Beszúr(ertek);
				count++;
			}

			public override string ToString()
			{
				string result = "";

				Elem<T> aktualis = fejelem.jobb; // "i=0"

				while (aktualis!=fejelem)  // "i<lista.Count"
				{
					result += aktualis.ertek.ToString()+" ";
					aktualis = aktualis.jobb;  // "i++"
				}

				return "[ " + result + "]";
			}

			public void Remove(T ertek) 
			{
				Elem<T> aktualis = fejelem.jobb; // "i=0"
				while (aktualis != fejelem && !aktualis.ertek.Equals(ertek))  // "i<lista.Count && ! lista[i]==ertek"
				{
					aktualis = aktualis.jobb;  // "i++"
				}

				if (aktualis != fejelem)
				{
					aktualis.Remove();
					count--;
				}

			}
			public void RemoveAll(T ertek)
			{
				Elem<T> aktualis = fejelem.jobb; // "i=0"
				while (aktualis != fejelem)  // "i<lista.Count"
				{
					if (aktualis.ertek.Equals(ertek))
					{
						aktualis.Remove();
						count--;
					}
					aktualis = aktualis.jobb;  // "i++"
				}
			}
			public void RemoveAt(int index)
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException("Elnézted az indexelést, vakegér! legyen nagyobb, mint nulla!");
				}

				if (Count <= index)
				{
					throw new IndexOutOfRangeException($"Elnézted az indexelést, vakegér! legyen kisebb, mint a lista mérete, ami most {Count}!");
				}

				int i = 0;
				Elem<T> aktualis = fejelem.jobb;
				while (i!=index)  
				{
					aktualis = aktualis.jobb;
					i++;
				}

				aktualis.Remove();
				count--;
			}
			public int Count { get => count; }
			//public int Count()
			//{
			//	return count;
			//}

			public List<T> ToList()
			{
				List<T> result = new List<T>(Count); // a Count miatt nem lesz resizing!

				Elem<T> aktualis = fejelem.jobb; // "i=0"

				while (aktualis != fejelem)  // "i<lista.Count"
				{
					result.Add(aktualis.ertek);
					aktualis = aktualis.jobb;  // "i++"
				}

				return result;
			}

			public T[] ToArray()
			{
				T[] result = new T[Count]; // a Count miatt nem lesz resizing!

				Elem<T> aktualis = fejelem.jobb; // "i=0"
				int i = 0;

				while (aktualis != fejelem)  // "i<lista.Count"
				{
					result[i] = aktualis.ertek;
					aktualis = aktualis.jobb;  // "i++"
					i++;
				}

				return result;
			}

			public int Megszamol(Func<T, bool> predikatum)
			{
				Elem<T> aktualis = fejelem.jobb;
				int db = 0;
				while (aktualis != fejelem)
				{
					aktualis = aktualis.jobb;
					if (predikatum(aktualis.ertek))
					{
						db++;
					}
				}
				return db;
			}

			public T First(Func<T, bool> predicate)
			{
				Elem<T> c = fejelem.jobb;
				while (c != fejelem && !predicate(c.ertek))
				{
					c = c.jobb;
				}

				if (c == fejelem)
				{
					throw new Exception("Nincs ilyen tulajdonságú elem!");
				}

				return c.ertek;
			}


			public int FindIndex(Func<T, bool> p)
			{
				Elem<T> e = fejelem.jobb;
				int i = 0;

				while (e != fejelem && !p(e.ertek))
				{
					e = e.jobb;
					i++;
				}

				if (e == fejelem) throw new Exception("Nincs ilyen tulajdonságú elem!");
				return i;
			}
		}
		static void Main(string[] args)
		{
			LancoltLista<int> lista = new LancoltLista<int>();

			lista.Add(5);
			lista.Add(6);
			lista.Add(7);
			lista.Add(8);


			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);

			lista.Remove(2);

			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);

			lista.Remove(6);

			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);

			lista.Remove(8);

			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);

			lista.RemoveAll(17);

			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);

			lista.RemoveAll(5);

			Console.WriteLine(lista);
			Console.WriteLine(lista.Count);
			Console.WriteLine(lista.Megszamol(x => x % 2 == 0));
			Console.WriteLine(lista.First(x => x % 2 == 0 ));
			//Console.WriteLine(lista.Last(x => x % 2 == 0));
			Console.WriteLine(lista.FindIndex(x => x % 2 == 0));
			//Console.WriteLine(lista.FindLastIndex(x => x % 2 == 0));
			//Console.WriteLine(lista.Where(x => x % 2 == 0));
			//Console.WriteLine(lista.Select(x => x + 2));
			//Console.WriteLine(lista.Contains(x => x % 2 == 0));
			//Console.WriteLine(lista.Max((x, y) => x.CompareTo(y)));
			//Console.WriteLine(lista.Min((x, y) => x.CompareTo(y)));

			//List<int> l = new List<int>();
		}
	}
}
