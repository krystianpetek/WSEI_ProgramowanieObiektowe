﻿using System.Collections;
using System.Globalization;

namespace PudelkoLib
{
    public sealed partial class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        public double A { get; init; } // dlugosc
        public double B { get; init; } // wysokosc
        public double C { get; init; } // szerokosc
        public string Objetosc => $"{Math.Round(A * B * C, 9):F9} m\u00B3";
        public string Pole => $"{Math.Round(2 * A * B + 2 * B * C + 2 * C * A, 6):F6} m\u00B3";
        public UnitOfMeasure Miara { get; init; }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double aNotNull = 0, bNotNull = 0, cNotNull = 0;

            #region METRY
            if (unit == UnitOfMeasure.meter)
            {
                if (a != null)
                    aNotNull = Math.Round((double)a, 3, MidpointRounding.ToZero);
                else
                    aNotNull = Math.Round(0.1000, 3);

                if (b != null)
                    bNotNull = Math.Round((double)b, 3, MidpointRounding.ToZero);
                else
                    bNotNull = Math.Round(0.1000, 3);

                if (c != null)
                    cNotNull = Math.Round((double)c, 3, MidpointRounding.ToZero);
                else
                    cNotNull = Math.Round(0.1000, 3);
            }
            #endregion

            #region CENTYMETRY
            if (unit == UnitOfMeasure.centimeter)
            {
                if (a != null)
                    aNotNull = Math.Round(((double)a / 100), 3, MidpointRounding.ToZero);
                else
                    aNotNull = Math.Round(0.1000, 3);

                if (b != null)
                    bNotNull = Math.Round(((double)b / 100), 3, MidpointRounding.ToZero);
                else
                    bNotNull = Math.Round(0.1000, 3);

                if (c != null)
                    cNotNull = Math.Round(((double)c / 100), 3, MidpointRounding.ToZero);
                else
                    cNotNull = Math.Round(0.1000, 3);
            }
            #endregion

            #region MILIMETRY
            if (unit == UnitOfMeasure.milimeter)
            {
                if (a != null)
                    aNotNull = Math.Round(((double)a / 1000), 3, MidpointRounding.ToZero);
                else
                    aNotNull = Math.Round(0.1000, 3);

                if (b != null)
                    bNotNull = Math.Round(((double)b / 1000), 3, MidpointRounding.ToZero);
                else
                    bNotNull = Math.Round(0.1000, 3);

                if (c != null)
                    cNotNull = Math.Round(((double)c / 1000), 3, MidpointRounding.ToZero);
                else
                    cNotNull = Math.Round(0.1000, 3);
            }
            #endregion
            
            #region WYJĄTKI
            if (aNotNull <= 0 || bNotNull <= 0 || cNotNull <= 0)
                throw new ArgumentOutOfRangeException();

            if (aNotNull > 10.000 || bNotNull > 10.000 || cNotNull > 10.000)
                throw new ArgumentOutOfRangeException();
            #endregion

            A = aNotNull;
            B = bNotNull;
            C = cNotNull;
        }
        #region ToString
        public override string ToString()
        {
            return this.ToString("m");
        }

        public string ToString(string? format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            char multiplicationSign = '\u00D7';
            string wynik = "";
            if (format == null || format == String.Empty)
                format = "m";
            if (format == "m")
                return $"{A:F3} m {multiplicationSign} {B:F3} m {multiplicationSign} {C:F3} m";
            else if (format == "cm")
                return $"{A * 100:F1} cm {multiplicationSign} {B * 100:F1} cm {multiplicationSign} {C * 100:F1} cm";
            else if (format == "mm")
                return $"{A * 1000:F0} mm {multiplicationSign} {B * 1000:F0} mm {multiplicationSign} {C * 1000:F0} mm";
            else
                throw new FormatException();
        }
        #endregion

        #region Equals
        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Pudelko);
        }

        public bool Equals(Pudelko? other)
        {
            Pudelko p1, p2;
            p1 = SortObject(this);
            p2 = SortObject(other);
            if (p1.A != p2.A || p1.B != p2.B || p1.C != p2.C)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.A, this.B, this.C);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !(p1 == p2);
        }
        #endregion

        #region Operator+
        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            var x = SortObject(p1);
            var y = SortObject(p2);
            var z = x.A;
            if (x.A > y.A)
                z = y.A;
            return new Pudelko(((x.A > y.A) ? x.A : y.A), ((x.B > y.B) ? x.B : y.B), ((x.C > y.C) ? x.C + z : y.C + z));
        }
        #endregion

        #region Konwersja
        public static explicit operator double[](Pudelko pudelko)
        {
            return new double[] { pudelko.A, pudelko.B, pudelko.C };
        }
        
        public static implicit operator Pudelko(ValueTuple<int, int, int> value)
        {
            return new Pudelko(value.Item1,value.Item2,value.Item3, UnitOfMeasure.milimeter);
        }
        #endregion

        #region Indexer
        public double this[int index]
        {
            get
            {
                if (index == 0) return this.A;
                else if (index == 1) return this.B;
                else if (index == 2) return this.C;
                else
                    throw new IndexOutOfRangeException("Pudełko ma tylko 3 wartości");
            }
        }
        #endregion
        
        #region Foreach iterator
        IEnumerator IEnumerable.GetEnumerator()
        {
            double[] tab = new double[] { this.A, this.B, this.C };
            foreach (var x in tab)
            {
                yield return x;
            }
        }
        #endregion

        private static Pudelko SortObject(Pudelko pudelko)
        {
            SortedSet<double> pudlo = new SortedSet<double>() { pudelko.A, pudelko.B, pudelko.C };
            return new Pudelko(pudlo.ElementAt(0), pudlo.ElementAt(1), pudlo.ElementAt(2));
        }
        public static Pudelko Parse(string x)
        {
            var split = x.Split(" ");
            double a = double.Parse(split[0].Replace('.',','));
            double b = double.Parse(split[3].Replace('.', ','));
            double c = double.Parse(split[6].Replace('.', ','));
            if (split[1] == "m" && split[4] == "m" && split[7] == "m")
                return new Pudelko(a, b, c, UnitOfMeasure.meter);
            else if (split[1] == "cm" && split[4] == "cm" && split[7] == "cm")
                return new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            else if (split[1] == "mm" && split[4] == "mm" && split[7] == "mm")
                return new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            else
                throw new InvalidDataException("Błędne dane do parsowania");
        }
    }
}