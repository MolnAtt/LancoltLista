using System;
using System.Data;
using System.Text;

namespace LancoltLista
{
	internal class Program
	{

		

		class LancoltLista<T>
		{
			class Elem<T>
			{
				Elem<T> bal; // rekurzív adatszerkezet!
				public T ertek;
				Elem<T> jobb;

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

			Elem<T> fejelem;

			public LancoltLista()
			{
				fejelem = new Elem<T>();
			}
		}
		static void Main(string[] args)
		{
			Elem e;

			e.jobb.jobb.jobb.jobb

		}
	}
}
