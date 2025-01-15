using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Microsoft.MachineLearning.Data.Conversion
{
	// Token: 0x02000285 RID: 645
	public sealed class Conversions
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0004EF63 File Offset: 0x0004D163
		public static Conversions Instance
		{
			get
			{
				if (Conversions._instance == null)
				{
					Interlocked.CompareExchange<Conversions>(ref Conversions._instance, new Conversions(), null);
				}
				return Conversions._instance;
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0004EF88 File Offset: 0x0004D188
		private Conversions()
		{
			this._kinds = new Dictionary<Type, DataKind>();
			for (DataKind dataKind = 1; dataKind < 17; dataKind++)
			{
				this._kinds.Add(DataKindExtensions.ToType(dataKind), dataKind);
			}
			this._delegatesStd = new Dictionary<int, Delegate>();
			this._delegatesAll = new Dictionary<int, Delegate>();
			this._isNADelegates = new Dictionary<DataKind, Delegate>();
			this._hasNADelegates = new Dictionary<DataKind, Delegate>();
			this._isDefaultDelegates = new Dictionary<DataKind, Delegate>();
			this._hasZeroDelegates = new Dictionary<DataKind, Delegate>();
			this._getNADelegates = new Dictionary<DataKind, Delegate>();
			this._tryParseDelegates = new Dictionary<DataKind, Delegate>();
			this.AddStd<DvInt1, DvInt1>(new ValueMapper<DvInt1, DvInt1>(this.Convert));
			this.AddStd<DvInt1, DvInt2>(new ValueMapper<DvInt1, DvInt2>(this.Convert));
			this.AddStd<DvInt1, DvInt4>(new ValueMapper<DvInt1, DvInt4>(this.Convert));
			this.AddStd<DvInt1, DvInt8>(new ValueMapper<DvInt1, DvInt8>(this.Convert));
			this.AddStd<DvInt1, float>(new ValueMapper<DvInt1, float>(this.Convert));
			this.AddStd<DvInt1, double>(new ValueMapper<DvInt1, double>(this.Convert));
			this.AddAux<DvInt1, StringBuilder>(new ValueMapper<DvInt1, StringBuilder>(this.Convert));
			this.AddStd<DvInt2, DvInt1>(new ValueMapper<DvInt2, DvInt1>(this.Convert));
			this.AddStd<DvInt2, DvInt2>(new ValueMapper<DvInt2, DvInt2>(this.Convert));
			this.AddStd<DvInt2, DvInt4>(new ValueMapper<DvInt2, DvInt4>(this.Convert));
			this.AddStd<DvInt2, DvInt8>(new ValueMapper<DvInt2, DvInt8>(this.Convert));
			this.AddStd<DvInt2, float>(new ValueMapper<DvInt2, float>(this.Convert));
			this.AddStd<DvInt2, double>(new ValueMapper<DvInt2, double>(this.Convert));
			this.AddAux<DvInt2, StringBuilder>(new ValueMapper<DvInt2, StringBuilder>(this.Convert));
			this.AddStd<DvInt4, DvInt1>(new ValueMapper<DvInt4, DvInt1>(this.Convert));
			this.AddStd<DvInt4, DvInt2>(new ValueMapper<DvInt4, DvInt2>(this.Convert));
			this.AddStd<DvInt4, DvInt4>(new ValueMapper<DvInt4, DvInt4>(this.Convert));
			this.AddStd<DvInt4, DvInt8>(new ValueMapper<DvInt4, DvInt8>(this.Convert));
			this.AddStd<DvInt4, float>(new ValueMapper<DvInt4, float>(this.Convert));
			this.AddStd<DvInt4, double>(new ValueMapper<DvInt4, double>(this.Convert));
			this.AddAux<DvInt4, StringBuilder>(new ValueMapper<DvInt4, StringBuilder>(this.Convert));
			this.AddStd<DvInt8, DvInt1>(new ValueMapper<DvInt8, DvInt1>(this.Convert));
			this.AddStd<DvInt8, DvInt2>(new ValueMapper<DvInt8, DvInt2>(this.Convert));
			this.AddStd<DvInt8, DvInt4>(new ValueMapper<DvInt8, DvInt4>(this.Convert));
			this.AddStd<DvInt8, DvInt8>(new ValueMapper<DvInt8, DvInt8>(this.Convert));
			this.AddStd<DvInt8, float>(new ValueMapper<DvInt8, float>(this.Convert));
			this.AddStd<DvInt8, double>(new ValueMapper<DvInt8, double>(this.Convert));
			this.AddAux<DvInt8, StringBuilder>(new ValueMapper<DvInt8, StringBuilder>(this.Convert));
			this.AddStd<byte, byte>(new ValueMapper<byte, byte>(this.Convert));
			this.AddStd<byte, ushort>(new ValueMapper<byte, ushort>(this.Convert));
			this.AddStd<byte, uint>(new ValueMapper<byte, uint>(this.Convert));
			this.AddStd<byte, ulong>(new ValueMapper<byte, ulong>(this.Convert));
			this.AddStd<byte, UInt128>(new ValueMapper<byte, UInt128>(this.Convert));
			this.AddStd<byte, float>(new ValueMapper<byte, float>(this.Convert));
			this.AddStd<byte, double>(new ValueMapper<byte, double>(this.Convert));
			this.AddAux<byte, StringBuilder>(new ValueMapper<byte, StringBuilder>(this.Convert));
			this.AddStd<ushort, byte>(new ValueMapper<ushort, byte>(this.Convert));
			this.AddStd<ushort, ushort>(new ValueMapper<ushort, ushort>(this.Convert));
			this.AddStd<ushort, uint>(new ValueMapper<ushort, uint>(this.Convert));
			this.AddStd<ushort, ulong>(new ValueMapper<ushort, ulong>(this.Convert));
			this.AddStd<ushort, UInt128>(new ValueMapper<ushort, UInt128>(this.Convert));
			this.AddStd<ushort, float>(new ValueMapper<ushort, float>(this.Convert));
			this.AddStd<ushort, double>(new ValueMapper<ushort, double>(this.Convert));
			this.AddAux<ushort, StringBuilder>(new ValueMapper<ushort, StringBuilder>(this.Convert));
			this.AddStd<uint, byte>(new ValueMapper<uint, byte>(this.Convert));
			this.AddStd<uint, ushort>(new ValueMapper<uint, ushort>(this.Convert));
			this.AddStd<uint, uint>(new ValueMapper<uint, uint>(this.Convert));
			this.AddStd<uint, ulong>(new ValueMapper<uint, ulong>(this.Convert));
			this.AddStd<uint, UInt128>(new ValueMapper<uint, UInt128>(this.Convert));
			this.AddStd<uint, float>(new ValueMapper<uint, float>(this.Convert));
			this.AddStd<uint, double>(new ValueMapper<uint, double>(this.Convert));
			this.AddAux<uint, StringBuilder>(new ValueMapper<uint, StringBuilder>(this.Convert));
			this.AddStd<ulong, byte>(new ValueMapper<ulong, byte>(this.Convert));
			this.AddStd<ulong, ushort>(new ValueMapper<ulong, ushort>(this.Convert));
			this.AddStd<ulong, uint>(new ValueMapper<ulong, uint>(this.Convert));
			this.AddStd<ulong, ulong>(new ValueMapper<ulong, ulong>(this.Convert));
			this.AddStd<ulong, UInt128>(new ValueMapper<ulong, UInt128>(this.Convert));
			this.AddStd<ulong, float>(new ValueMapper<ulong, float>(this.Convert));
			this.AddStd<ulong, double>(new ValueMapper<ulong, double>(this.Convert));
			this.AddAux<ulong, StringBuilder>(new ValueMapper<ulong, StringBuilder>(this.Convert));
			this.AddStd<UInt128, byte>(new ValueMapper<UInt128, byte>(this.Convert));
			this.AddStd<UInt128, ushort>(new ValueMapper<UInt128, ushort>(this.Convert));
			this.AddStd<UInt128, uint>(new ValueMapper<UInt128, uint>(this.Convert));
			this.AddStd<UInt128, ulong>(new ValueMapper<UInt128, ulong>(this.Convert));
			this.AddAux<UInt128, StringBuilder>(new ValueMapper<UInt128, StringBuilder>(this.Convert));
			this.AddStd<float, float>(new ValueMapper<float, float>(this.Convert));
			this.AddStd<float, double>(new ValueMapper<float, double>(this.Convert));
			this.AddAux<float, StringBuilder>(new ValueMapper<float, StringBuilder>(this.Convert));
			this.AddStd<double, float>(new ValueMapper<double, float>(this.Convert));
			this.AddStd<double, double>(new ValueMapper<double, double>(this.Convert));
			this.AddAux<double, StringBuilder>(new ValueMapper<double, StringBuilder>(this.Convert));
			this.AddStd<DvText, DvInt1>(new ValueMapper<DvText, DvInt1>(this.Convert));
			this.AddStd<DvText, byte>(new ValueMapper<DvText, byte>(this.Convert));
			this.AddStd<DvText, DvInt2>(new ValueMapper<DvText, DvInt2>(this.Convert));
			this.AddStd<DvText, ushort>(new ValueMapper<DvText, ushort>(this.Convert));
			this.AddStd<DvText, DvInt4>(new ValueMapper<DvText, DvInt4>(this.Convert));
			this.AddStd<DvText, uint>(new ValueMapper<DvText, uint>(this.Convert));
			this.AddStd<DvText, DvInt8>(new ValueMapper<DvText, DvInt8>(this.Convert));
			this.AddStd<DvText, ulong>(new ValueMapper<DvText, ulong>(this.Convert));
			this.AddStd<DvText, UInt128>(new ValueMapper<DvText, UInt128>(this.Convert));
			this.AddStd<DvText, float>(new ValueMapper<DvText, float>(this.Convert));
			this.AddStd<DvText, double>(new ValueMapper<DvText, double>(this.Convert));
			this.AddStd<DvText, DvText>(new ValueMapper<DvText, DvText>(this.Convert));
			this.AddStd<DvText, DvBool>(new ValueMapper<DvText, DvBool>(this.Convert));
			this.AddAux<DvText, StringBuilder>(new ValueMapper<DvText, StringBuilder>(this.Convert));
			this.AddStd<DvText, DvTimeSpan>(new ValueMapper<DvText, DvTimeSpan>(this.Convert));
			this.AddStd<DvText, DvDateTime>(new ValueMapper<DvText, DvDateTime>(this.Convert));
			this.AddStd<DvText, DvDateTimeZone>(new ValueMapper<DvText, DvDateTimeZone>(this.Convert));
			this.AddStd<DvBool, DvInt1>(new ValueMapper<DvBool, DvInt1>(this.Convert));
			this.AddStd<DvBool, DvInt2>(new ValueMapper<DvBool, DvInt2>(this.Convert));
			this.AddStd<DvBool, DvInt4>(new ValueMapper<DvBool, DvInt4>(this.Convert));
			this.AddStd<DvBool, DvInt8>(new ValueMapper<DvBool, DvInt8>(this.Convert));
			this.AddStd<DvBool, float>(new ValueMapper<DvBool, float>(this.Convert));
			this.AddStd<DvBool, double>(new ValueMapper<DvBool, double>(this.Convert));
			this.AddStd<DvBool, DvBool>(new ValueMapper<DvBool, DvBool>(this.Convert));
			this.AddAux<DvBool, StringBuilder>(new ValueMapper<DvBool, StringBuilder>(this.Convert));
			this.AddStd<DvTimeSpan, DvInt8>(new ValueMapper<DvTimeSpan, DvInt8>(this.Convert));
			this.AddStd<DvTimeSpan, float>(new ValueMapper<DvTimeSpan, float>(this.Convert));
			this.AddStd<DvTimeSpan, double>(new ValueMapper<DvTimeSpan, double>(this.Convert));
			this.AddAux<DvTimeSpan, StringBuilder>(new ValueMapper<DvTimeSpan, StringBuilder>(this.Convert));
			this.AddStd<DvDateTime, DvInt8>(new ValueMapper<DvDateTime, DvInt8>(this.Convert));
			this.AddStd<DvDateTime, float>(new ValueMapper<DvDateTime, float>(this.Convert));
			this.AddStd<DvDateTime, double>(new ValueMapper<DvDateTime, double>(this.Convert));
			this.AddAux<DvDateTime, StringBuilder>(new ValueMapper<DvDateTime, StringBuilder>(this.Convert));
			this.AddStd<DvDateTimeZone, DvInt8>(new ValueMapper<DvDateTimeZone, DvInt8>(this.Convert));
			this.AddStd<DvDateTimeZone, float>(new ValueMapper<DvDateTimeZone, float>(this.Convert));
			this.AddStd<DvDateTimeZone, double>(new ValueMapper<DvDateTimeZone, double>(this.Convert));
			this.AddAux<DvDateTimeZone, StringBuilder>(new ValueMapper<DvDateTimeZone, StringBuilder>(this.Convert));
			this.AddIsNA<DvInt1>(new RefPredicate<DvInt1>(this.IsNA));
			this.AddIsNA<DvInt2>(new RefPredicate<DvInt2>(this.IsNA));
			this.AddIsNA<DvInt4>(new RefPredicate<DvInt4>(this.IsNA));
			this.AddIsNA<DvInt8>(new RefPredicate<DvInt8>(this.IsNA));
			this.AddIsNA<float>(new RefPredicate<float>(this.IsNA));
			this.AddIsNA<double>(new RefPredicate<double>(this.IsNA));
			this.AddIsNA<DvBool>(new RefPredicate<DvBool>(this.IsNA));
			this.AddIsNA<DvText>(new RefPredicate<DvText>(this.IsNA));
			this.AddIsNA<DvTimeSpan>(new RefPredicate<DvTimeSpan>(this.IsNA));
			this.AddIsNA<DvDateTime>(new RefPredicate<DvDateTime>(this.IsNA));
			this.AddIsNA<DvDateTimeZone>(new RefPredicate<DvDateTimeZone>(this.IsNA));
			this.AddGetNA<DvInt1>(new ValueGetter<DvInt1>(this.GetNA));
			this.AddGetNA<DvInt2>(new ValueGetter<DvInt2>(this.GetNA));
			this.AddGetNA<DvInt4>(new ValueGetter<DvInt4>(this.GetNA));
			this.AddGetNA<DvInt8>(new ValueGetter<DvInt8>(this.GetNA));
			this.AddGetNA<float>(new ValueGetter<float>(this.GetNA));
			this.AddGetNA<double>(new ValueGetter<double>(this.GetNA));
			this.AddGetNA<DvBool>(new ValueGetter<DvBool>(this.GetNA));
			this.AddGetNA<DvText>(new ValueGetter<DvText>(this.GetNA));
			this.AddGetNA<DvTimeSpan>(new ValueGetter<DvTimeSpan>(this.GetNA));
			this.AddGetNA<DvDateTime>(new ValueGetter<DvDateTime>(this.GetNA));
			this.AddGetNA<DvDateTimeZone>(new ValueGetter<DvDateTimeZone>(this.GetNA));
			this.AddHasNA<DvInt1>(new RefPredicate<VBuffer<DvInt1>>(this.HasNA));
			this.AddHasNA<DvInt2>(new RefPredicate<VBuffer<DvInt2>>(this.HasNA));
			this.AddHasNA<DvInt4>(new RefPredicate<VBuffer<DvInt4>>(this.HasNA));
			this.AddHasNA<DvInt8>(new RefPredicate<VBuffer<DvInt8>>(this.HasNA));
			this.AddHasNA<float>(new RefPredicate<VBuffer<float>>(this.HasNA));
			this.AddHasNA<double>(new RefPredicate<VBuffer<double>>(this.HasNA));
			this.AddHasNA<DvBool>(new RefPredicate<VBuffer<DvBool>>(this.HasNA));
			this.AddHasNA<DvText>(new RefPredicate<VBuffer<DvText>>(this.HasNA));
			this.AddHasNA<DvTimeSpan>(new RefPredicate<VBuffer<DvTimeSpan>>(this.HasNA));
			this.AddHasNA<DvDateTime>(new RefPredicate<VBuffer<DvDateTime>>(this.HasNA));
			this.AddHasNA<DvDateTimeZone>(new RefPredicate<VBuffer<DvDateTimeZone>>(this.HasNA));
			this.AddIsDef<DvInt1>(new RefPredicate<DvInt1>(this.IsDefault));
			this.AddIsDef<DvInt2>(new RefPredicate<DvInt2>(this.IsDefault));
			this.AddIsDef<DvInt4>(new RefPredicate<DvInt4>(this.IsDefault));
			this.AddIsDef<DvInt8>(new RefPredicate<DvInt8>(this.IsDefault));
			this.AddIsDef<float>(new RefPredicate<float>(this.IsDefault));
			this.AddIsDef<double>(new RefPredicate<double>(this.IsDefault));
			this.AddIsDef<DvBool>(new RefPredicate<DvBool>(this.IsDefault));
			this.AddIsDef<DvText>(new RefPredicate<DvText>(this.IsDefault));
			this.AddIsDef<byte>(new RefPredicate<byte>(this.IsDefault));
			this.AddIsDef<ushort>(new RefPredicate<ushort>(this.IsDefault));
			this.AddIsDef<uint>(new RefPredicate<uint>(this.IsDefault));
			this.AddIsDef<ulong>(new RefPredicate<ulong>(this.IsDefault));
			this.AddIsDef<UInt128>(new RefPredicate<UInt128>(this.IsDefault));
			this.AddIsDef<DvTimeSpan>(new RefPredicate<DvTimeSpan>(this.IsDefault));
			this.AddIsDef<DvDateTime>(new RefPredicate<DvDateTime>(this.IsDefault));
			this.AddIsDef<DvDateTimeZone>(new RefPredicate<DvDateTimeZone>(this.IsDefault));
			this.AddHasZero<byte>(new RefPredicate<VBuffer<byte>>(this.HasZero));
			this.AddHasZero<ushort>(new RefPredicate<VBuffer<ushort>>(this.HasZero));
			this.AddHasZero<uint>(new RefPredicate<VBuffer<uint>>(this.HasZero));
			this.AddHasZero<ulong>(new RefPredicate<VBuffer<ulong>>(this.HasZero));
			this.AddTryParse<DvInt1>(new TryParseMapper<DvInt1>(this.TryParse));
			this.AddTryParse<DvInt2>(new TryParseMapper<DvInt2>(this.TryParse));
			this.AddTryParse<DvInt4>(new TryParseMapper<DvInt4>(this.TryParse));
			this.AddTryParse<DvInt8>(new TryParseMapper<DvInt8>(this.TryParse));
			this.AddTryParse<byte>(new TryParseMapper<byte>(this.TryParse));
			this.AddTryParse<ushort>(new TryParseMapper<ushort>(this.TryParse));
			this.AddTryParse<uint>(new TryParseMapper<uint>(this.TryParse));
			this.AddTryParse<ulong>(new TryParseMapper<ulong>(this.TryParse));
			this.AddTryParse<UInt128>(new TryParseMapper<UInt128>(this.TryParse));
			this.AddTryParse<float>(new TryParseMapper<float>(this.TryParse));
			this.AddTryParse<double>(new TryParseMapper<double>(this.TryParse));
			this.AddTryParse<DvBool>(new TryParseMapper<DvBool>(this.TryParse));
			this.AddTryParse<DvText>(new TryParseMapper<DvText>(this.TryParse));
			this.AddTryParse<DvTimeSpan>(new TryParseMapper<DvTimeSpan>(this.TryParse));
			this.AddTryParse<DvDateTime>(new TryParseMapper<DvDateTime>(this.TryParse));
			this.AddTryParse<DvDateTimeZone>(new TryParseMapper<DvDateTimeZone>(this.TryParse));
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0004FC90 File Offset: 0x0004DE90
		private static int GetKey(DataKind kindSrc, DataKind kindDst)
		{
			return (kindSrc << 8) | kindDst;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0004FC98 File Offset: 0x0004DE98
		private void AddStd<TSrc, TDst>(ValueMapper<TSrc, TDst> fn)
		{
			DataKind dataKind = this._kinds[typeof(TSrc)];
			DataKind dataKind2 = this._kinds[typeof(TDst)];
			int key = Conversions.GetKey(dataKind, dataKind2);
			this._delegatesStd.Add(key, fn);
			this._delegatesAll.Add(key, fn);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		private void AddAux<TSrc, TDst>(ValueMapper<TSrc, TDst> fn)
		{
			DataKind dataKind = this._kinds[typeof(TSrc)];
			DataKind dataKind2 = ((typeof(TDst) == typeof(StringBuilder)) ? 100 : this._kinds[typeof(TDst)]);
			this._delegatesAll.Add(Conversions.GetKey(dataKind, dataKind2), fn);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0004FD60 File Offset: 0x0004DF60
		private void AddIsNA<T>(RefPredicate<T> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._isNADelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0004FD90 File Offset: 0x0004DF90
		private void AddGetNA<T>(ValueGetter<T> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._getNADelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0004FDC0 File Offset: 0x0004DFC0
		private void AddHasNA<T>(RefPredicate<VBuffer<T>> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._hasNADelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0004FDF0 File Offset: 0x0004DFF0
		private void AddIsDef<T>(RefPredicate<T> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._isDefaultDelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0004FE20 File Offset: 0x0004E020
		private void AddHasZero<T>(RefPredicate<VBuffer<T>> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._hasZeroDelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0004FE50 File Offset: 0x0004E050
		private void AddTryParse<T>(TryParseMapper<T> fn)
		{
			DataKind dataKind = this._kinds[typeof(T)];
			this._tryParseDelegates.Add(dataKind, fn);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0004FE80 File Offset: 0x0004E080
		public ValueMapper<TSrc, TDst> GetStandardConversion<TSrc, TDst>(ColumnType typeSrc, ColumnType typeDst, out bool identity)
		{
			ValueMapper<TSrc, TDst> valueMapper;
			if (!this.TryGetStandardConversion<TSrc, TDst>(typeSrc, typeDst, out valueMapper, out identity))
			{
				throw Contracts.Except("No standard conversion from '{0}' to '{1}'", new object[] { typeSrc, typeDst });
			}
			return valueMapper;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		public bool TryGetStandardConversion<TSrc, TDst>(ColumnType typeSrc, ColumnType typeDst, out ValueMapper<TSrc, TDst> conv, out bool identity)
		{
			Contracts.CheckValue<ColumnType>(typeSrc, "typeSrc");
			Contracts.CheckValue<ColumnType>(typeDst, "typeDst");
			Contracts.Check(typeSrc.RawType == typeof(TSrc));
			Contracts.Check(typeDst.RawType == typeof(TDst));
			Delegate @delegate;
			if (!this.TryGetStandardConversion(typeSrc, typeDst, out @delegate, out identity))
			{
				conv = null;
				return false;
			}
			conv = (ValueMapper<TSrc, TDst>)@delegate;
			return true;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0004FF2C File Offset: 0x0004E12C
		public Delegate GetStandardConversion(ColumnType typeSrc, ColumnType typeDst)
		{
			Delegate @delegate;
			bool flag;
			if (!this.TryGetStandardConversion(typeSrc, typeDst, out @delegate, out flag))
			{
				throw Contracts.Except("No standard conversion from '{0}' to '{1}'", new object[] { typeSrc, typeDst });
			}
			return @delegate;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0004FF64 File Offset: 0x0004E164
		public bool TryGetStandardConversion(ColumnType typeSrc, ColumnType typeDst, out Delegate conv, out bool identity)
		{
			Contracts.CheckValue<ColumnType>(typeSrc, "typeSrc");
			Contracts.CheckValue<ColumnType>(typeDst, "typeDst");
			conv = null;
			identity = false;
			if (typeSrc.IsKey)
			{
				KeyType asKey = typeSrc.AsKey;
				if (typeDst.IsKey)
				{
					KeyType asKey2 = typeDst.AsKey;
					if (asKey.Count != asKey2.Count)
					{
						return false;
					}
					if (asKey.Count == 0 && asKey.RawKind > asKey2.RawKind)
					{
						return false;
					}
					if (asKey.Contiguous != asKey2.Contiguous)
					{
						return false;
					}
				}
				else
				{
					if (!KeyType.IsValidDataKind(typeDst.RawKind))
					{
						return false;
					}
					if (asKey.RawKind > typeDst.RawKind)
					{
						if (asKey.Count == 0)
						{
							return false;
						}
						if ((long)asKey.Count > (long)DataKindExtensions.ToMaxInt(typeDst.RawKind))
						{
							return false;
						}
					}
				}
			}
			else if (typeDst.IsKey)
			{
				if (!typeSrc.IsText)
				{
					return false;
				}
				conv = this.GetKeyParse(typeDst.AsKey);
				return true;
			}
			else if (!typeDst.IsStandardScalar)
			{
				return false;
			}
			int key = Conversions.GetKey(typeSrc.RawKind, typeDst.RawKind);
			identity = typeSrc.RawKind == typeDst.RawKind;
			return this._delegatesStd.TryGetValue(key, out conv);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00050080 File Offset: 0x0004E280
		public ValueMapper<TSrc, StringBuilder> GetStringConversion<TSrc>(ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.Check(type.RawType == typeof(TSrc), "Wrong TSrc type argument");
			if (type.IsKey)
			{
				return this.GetKeyStringConversion<TSrc>(type.AsKey);
			}
			if (!type.IsStandardScalar)
			{
				throw Contracts.Except("No conversion from '{0}' to StringBuilder", new object[] { type });
			}
			return this.GetStringConversion<TSrc>();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000500F4 File Offset: 0x0004E2F4
		public ValueMapper<TSrc, StringBuilder> GetStringConversion<TSrc>()
		{
			DataKind dataKind;
			Contracts.Check(this._kinds.TryGetValue(typeof(TSrc), out dataKind));
			int key = Conversions.GetKey(dataKind, 100);
			Delegate @delegate;
			if (!this._delegatesAll.TryGetValue(key, out @delegate))
			{
				throw Contracts.Except("No conversion from '{0}' to StringBuilder", new object[] { dataKind });
			}
			return (ValueMapper<TSrc, StringBuilder>)@delegate;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00050210 File Offset: 0x0004E410
		public ValueMapper<TSrc, StringBuilder> GetKeyStringConversion<TSrc>(KeyType key)
		{
			Contracts.Check(key.RawType == typeof(TSrc));
			ulong min = key.Min;
			int count = key.Count;
			bool flag;
			ValueMapper<TSrc, ulong> convSrc = this.GetStandardConversion<TSrc, ulong>(key, NumberType.U8, out flag);
			ValueMapper<ulong, StringBuilder> convU8 = this.GetStringConversion<ulong>();
			if (count > 0)
			{
				return delegate(ref TSrc src, ref StringBuilder dst)
				{
					ulong num = 0UL;
					convSrc.Invoke(ref src, ref num);
					if (num == 0UL || num > (ulong)((long)count))
					{
						Conversions.ClearDst(ref dst);
						return;
					}
					num = num + min - 1UL;
					convU8.Invoke(ref num, ref dst);
				};
			}
			return delegate(ref TSrc src, ref StringBuilder dst)
			{
				ulong num2 = 0UL;
				convSrc.Invoke(ref src, ref num2);
				if (num2 == 0UL || (min > 1UL && num2 > 18446744073709551615UL - min + 1UL))
				{
					Conversions.ClearDst(ref dst);
					return;
				}
				num2 = num2 + min - 1UL;
				convU8.Invoke(ref num2, ref dst);
			};
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000502A8 File Offset: 0x0004E4A8
		public TryParseMapper<TDst> GetParseConversion<TDst>(ColumnType typeDst)
		{
			Contracts.CheckValue<ColumnType>(typeDst, "typeDst");
			Contracts.CheckParam(typeDst.IsStandardScalar || typeDst.IsKey, "typeDst", "Parse conversion only supported for standard types");
			Contracts.Check(typeDst.RawType == typeof(TDst), "Wrong TDst type parameter");
			if (typeDst.IsKey)
			{
				return this.GetKeyTryParse<TDst>(typeDst.AsKey);
			}
			return (TryParseMapper<TDst>)this._tryParseDelegates[typeDst.RawKind];
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00050378 File Offset: 0x0004E578
		private TryParseMapper<TDst> GetKeyTryParse<TDst>(KeyType key)
		{
			ulong min = key.Min;
			ulong num = DataKindExtensions.ToMaxInt(key.RawKind);
			ulong max;
			if (key.Count > 0)
			{
				max = min - 1UL + (ulong)((long)key.Count);
			}
			else if (min == 0UL)
			{
				max = num - 1UL;
			}
			else if (key.RawKind == 8)
			{
				max = ulong.MaxValue;
			}
			else if (min - 1UL > 18446744073709551615UL - num)
			{
				max = ulong.MaxValue;
			}
			else
			{
				max = min - 1UL + num;
			}
			bool flag;
			ValueMapper<ulong, TDst> fnConv = this.GetStandardConversion<ulong, TDst>(NumberType.U8, NumberType.FromKind(key.RawKind), out flag);
			return delegate(ref DvText src, out TDst dst)
			{
				dst = default(TDst);
				ulong num2;
				if (!this.TryParseKey(ref src, min, max, out num2))
				{
					return false;
				}
				fnConv.Invoke(ref num2, ref dst);
				return true;
			};
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0005044C File Offset: 0x0004E64C
		private Delegate GetKeyParse(KeyType key)
		{
			Func<KeyType, ValueMapper<DvText, int>> func = new Func<KeyType, ValueMapper<DvText, int>>(this.GetKeyParse<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { key.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { key });
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000504F0 File Offset: 0x0004E6F0
		private ValueMapper<DvText, TDst> GetKeyParse<TDst>(KeyType key)
		{
			ulong min = key.Min;
			ulong num = DataKindExtensions.ToMaxInt(key.RawKind);
			ulong max;
			if (key.Count > 0)
			{
				max = min - 1UL + (ulong)((long)key.Count);
			}
			else if (min == 0UL)
			{
				max = num - 1UL;
			}
			else if (key.RawKind == 8)
			{
				max = ulong.MaxValue;
			}
			else if (min - 1UL > 18446744073709551615UL - num)
			{
				max = ulong.MaxValue;
			}
			else
			{
				max = min - 1UL + num;
			}
			bool flag;
			ValueMapper<ulong, TDst> fnConv = this.GetStandardConversion<ulong, TDst>(NumberType.U8, NumberType.FromKind(key.RawKind), out flag);
			return delegate(ref DvText src, ref TDst dst)
			{
				dst = default(TDst);
				ulong num2;
				if (!this.TryParseKey(ref src, min, max, out num2))
				{
					dst = default(TDst);
					return;
				}
				fnConv.Invoke(ref num2, ref dst);
			};
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000505C1 File Offset: 0x0004E7C1
		private static StringBuilder ClearDst(ref StringBuilder dst)
		{
			if (dst == null)
			{
				dst = new StringBuilder();
			}
			else
			{
				dst.Clear();
			}
			return dst;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000505DC File Offset: 0x0004E7DC
		public RefPredicate<T> GetIsDefaultPredicate<T>(ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(!type.IsVector, "type");
			Contracts.CheckParam(type.RawType == typeof(T), "type");
			Delegate @delegate;
			if ((!type.IsStandardScalar && !type.IsKey) || !this._isDefaultDelegates.TryGetValue(type.RawKind, out @delegate))
			{
				throw Contracts.Except("No IsDefault predicate for '{0}'", new object[] { type });
			}
			return (RefPredicate<T>)@delegate;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0005066C File Offset: 0x0004E86C
		public RefPredicate<T> GetIsNAPredicate<T>(ColumnType type)
		{
			RefPredicate<T> refPredicate;
			if (this.TryGetIsNAPredicate<T>(type, out refPredicate))
			{
				return refPredicate;
			}
			throw Contracts.Except("No IsNA predicate for '{0}'", new object[] { type });
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0005069C File Offset: 0x0004E89C
		public bool TryGetIsNAPredicate<T>(ColumnType type, out RefPredicate<T> pred)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(type.RawType == typeof(T), "type");
			Delegate @delegate;
			if (!this.TryGetIsNAPredicate(type, out @delegate))
			{
				pred = null;
				return false;
			}
			pred = (RefPredicate<T>)@delegate;
			return true;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000506EC File Offset: 0x0004E8EC
		public bool TryGetIsNAPredicate(ColumnType type, out Delegate del)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(!type.IsVector, "type");
			if (type.IsKey)
			{
				del = this._isDefaultDelegates[type.RawKind];
			}
			else if (!type.IsStandardScalar || !this._isNADelegates.TryGetValue(type.RawKind, out del))
			{
				del = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00050758 File Offset: 0x0004E958
		public RefPredicate<VBuffer<T>> GetHasMissingPredicate<T>(VectorType type)
		{
			Contracts.CheckValue<VectorType>(type, "type");
			Contracts.CheckParam(type.ItemType.RawType == typeof(T), "type");
			PrimitiveType itemType = type.ItemType;
			Delegate @delegate;
			if (itemType.IsKey)
			{
				@delegate = this._hasZeroDelegates[itemType.RawKind];
			}
			else if (!itemType.IsStandardScalar || !this._hasNADelegates.TryGetValue(itemType.RawKind, out @delegate))
			{
				throw Contracts.Except("No HasMissing predicate for '{0}'", new object[] { type });
			}
			return (RefPredicate<VBuffer<T>>)@delegate;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000507F4 File Offset: 0x0004E9F4
		public T GetNAOrDefault<T>(ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(type.RawType == typeof(T), "type");
			Delegate @delegate;
			if (!this._getNADelegates.TryGetValue(type.RawKind, out @delegate))
			{
				return default(T);
			}
			T t = default(T);
			((ValueGetter<T>)@delegate).Invoke(ref t);
			return t;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00050860 File Offset: 0x0004EA60
		public T GetNAOrDefault<T>(ColumnType type, out bool isDefault)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(type.RawType == typeof(T), "type");
			Delegate @delegate;
			if (!this._getNADelegates.TryGetValue(type.RawKind, out @delegate))
			{
				isDefault = true;
				return default(T);
			}
			T t = default(T);
			((ValueGetter<T>)@delegate).Invoke(ref t);
			isDefault = false;
			return t;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000508DC File Offset: 0x0004EADC
		public ValueGetter<T> GetNAOrDefaultGetter<T>(ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(type, "type");
			Contracts.CheckParam(type.RawType == typeof(T), "type");
			Delegate @delegate;
			if (!this._getNADelegates.TryGetValue(type.RawKind, out @delegate))
			{
				return delegate(ref T res)
				{
					res = default(T);
				};
			}
			return (ValueGetter<T>)@delegate;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0005093B File Offset: 0x0004EB3B
		private bool IsNA(ref DvInt1 src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00050943 File Offset: 0x0004EB43
		private bool IsNA(ref DvInt2 src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0005094B File Offset: 0x0004EB4B
		private bool IsNA(ref DvInt4 src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00050953 File Offset: 0x0004EB53
		private bool IsNA(ref DvInt8 src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0005095B File Offset: 0x0004EB5B
		private bool IsNA(ref float src)
		{
			return TypeUtils.IsNA(src);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00050964 File Offset: 0x0004EB64
		private bool IsNA(ref double src)
		{
			return TypeUtils.IsNA(src);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0005096D File Offset: 0x0004EB6D
		private bool IsNA(ref DvBool src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00050975 File Offset: 0x0004EB75
		private bool IsNA(ref DvTimeSpan src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0005097D File Offset: 0x0004EB7D
		private bool IsNA(ref DvDateTime src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00050985 File Offset: 0x0004EB85
		private bool IsNA(ref DvDateTimeZone src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0005098D File Offset: 0x0004EB8D
		private bool IsNA(ref DvText src)
		{
			return src.IsNA;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00050998 File Offset: 0x0004EB98
		private bool HasNA(ref VBuffer<DvInt1> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000509CC File Offset: 0x0004EBCC
		private bool HasNA(ref VBuffer<DvInt2> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00050A00 File Offset: 0x0004EC00
		private bool HasNA(ref VBuffer<DvInt4> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00050A34 File Offset: 0x0004EC34
		private bool HasNA(ref VBuffer<DvInt8> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00050A68 File Offset: 0x0004EC68
		private bool HasNA(ref VBuffer<float> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (TypeUtils.IsNA(src.Values[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00050A98 File Offset: 0x0004EC98
		private bool HasNA(ref VBuffer<double> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (TypeUtils.IsNA(src.Values[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00050AC8 File Offset: 0x0004ECC8
		private bool HasNA(ref VBuffer<DvBool> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00050AFC File Offset: 0x0004ECFC
		private bool HasNA(ref VBuffer<DvTimeSpan> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00050B30 File Offset: 0x0004ED30
		private bool HasNA(ref VBuffer<DvDateTime> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00050B64 File Offset: 0x0004ED64
		private bool HasNA(ref VBuffer<DvDateTimeZone> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00050B98 File Offset: 0x0004ED98
		private bool HasNA(ref VBuffer<DvText> src)
		{
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i].IsNA)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00050BCC File Offset: 0x0004EDCC
		private bool IsDefault(ref DvInt1 src)
		{
			return src.RawValue == 0;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00050BD7 File Offset: 0x0004EDD7
		private bool IsDefault(ref DvInt2 src)
		{
			return src.RawValue == 0;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00050BE2 File Offset: 0x0004EDE2
		private bool IsDefault(ref DvInt4 src)
		{
			return src.RawValue == 0;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00050BED File Offset: 0x0004EDED
		private bool IsDefault(ref DvInt8 src)
		{
			return src.RawValue == 0L;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00050BF9 File Offset: 0x0004EDF9
		private bool IsDefault(ref float src)
		{
			return src == 0f;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00050C04 File Offset: 0x0004EE04
		private bool IsDefault(ref double src)
		{
			return src == 0.0;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00050C13 File Offset: 0x0004EE13
		private bool IsDefault(ref DvText src)
		{
			return src.IsEmpty;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00050C1B File Offset: 0x0004EE1B
		private bool IsDefault(ref DvBool src)
		{
			return src.IsFalse;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00050C23 File Offset: 0x0004EE23
		private bool IsDefault(ref byte src)
		{
			return src == 0;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00050C2A File Offset: 0x0004EE2A
		private bool IsDefault(ref ushort src)
		{
			return src == 0;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00050C31 File Offset: 0x0004EE31
		private bool IsDefault(ref uint src)
		{
			return src == 0U;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00050C38 File Offset: 0x0004EE38
		private bool IsDefault(ref ulong src)
		{
			return src == 0UL;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00050C40 File Offset: 0x0004EE40
		private bool IsDefault(ref UInt128 src)
		{
			return src.Equals(default(UInt128));
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00050C5C File Offset: 0x0004EE5C
		private bool IsDefault(ref DvTimeSpan src)
		{
			return src.Equals(default(DvTimeSpan));
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00050C78 File Offset: 0x0004EE78
		private bool IsDefault(ref DvDateTime src)
		{
			return src.Equals(default(DvDateTime));
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00050C94 File Offset: 0x0004EE94
		private bool IsDefault(ref DvDateTimeZone src)
		{
			return src.Equals(default(DvDateTimeZone));
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00050CB0 File Offset: 0x0004EEB0
		private bool HasZero(ref VBuffer<byte> src)
		{
			if (!src.IsDense)
			{
				return true;
			}
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i] == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00050CE8 File Offset: 0x0004EEE8
		private bool HasZero(ref VBuffer<ushort> src)
		{
			if (!src.IsDense)
			{
				return true;
			}
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i] == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00050D20 File Offset: 0x0004EF20
		private bool HasZero(ref VBuffer<uint> src)
		{
			if (!src.IsDense)
			{
				return true;
			}
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i] == 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00050D58 File Offset: 0x0004EF58
		private bool HasZero(ref VBuffer<ulong> src)
		{
			if (!src.IsDense)
			{
				return true;
			}
			for (int i = 0; i < src.Count; i++)
			{
				if (src.Values[i] == 0UL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00050D8F File Offset: 0x0004EF8F
		private void GetNA(ref DvInt1 value)
		{
			value = DvInt1.NA;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00050D9C File Offset: 0x0004EF9C
		private void GetNA(ref DvInt2 value)
		{
			value = DvInt2.NA;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00050DA9 File Offset: 0x0004EFA9
		private void GetNA(ref DvInt4 value)
		{
			value = DvInt4.NA;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00050DB6 File Offset: 0x0004EFB6
		private void GetNA(ref DvInt8 value)
		{
			value = DvInt8.NA;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00050DC3 File Offset: 0x0004EFC3
		private void GetNA(ref float value)
		{
			value = float.NaN;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00050DCC File Offset: 0x0004EFCC
		private void GetNA(ref double value)
		{
			value = double.NaN;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00050DD9 File Offset: 0x0004EFD9
		private void GetNA(ref DvBool value)
		{
			value = DvBool.NA;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00050DE6 File Offset: 0x0004EFE6
		private void GetNA(ref DvTimeSpan value)
		{
			value = DvTimeSpan.NA;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00050DF3 File Offset: 0x0004EFF3
		private void GetNA(ref DvDateTime value)
		{
			value = DvDateTime.NA;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00050E00 File Offset: 0x0004F000
		private void GetNA(ref DvDateTimeZone value)
		{
			value = DvDateTimeZone.NA;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00050E0D File Offset: 0x0004F00D
		private void GetNA(ref DvText value)
		{
			value = DvText.NA;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00050E1A File Offset: 0x0004F01A
		public void Convert(ref DvInt1 src, ref DvInt1 dst)
		{
			dst = src;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00050E28 File Offset: 0x0004F028
		public void Convert(ref DvInt2 src, ref DvInt1 dst)
		{
			dst = (DvInt1)src;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00050E3B File Offset: 0x0004F03B
		public void Convert(ref DvInt4 src, ref DvInt1 dst)
		{
			dst = (DvInt1)src;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00050E4E File Offset: 0x0004F04E
		public void Convert(ref DvInt8 src, ref DvInt1 dst)
		{
			dst = (DvInt1)src;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00050E61 File Offset: 0x0004F061
		public void Convert(ref DvInt1 src, ref DvInt2 dst)
		{
			dst = src;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00050E74 File Offset: 0x0004F074
		public void Convert(ref DvInt2 src, ref DvInt2 dst)
		{
			dst = src;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00050E82 File Offset: 0x0004F082
		public void Convert(ref DvInt4 src, ref DvInt2 dst)
		{
			dst = (DvInt2)src;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00050E95 File Offset: 0x0004F095
		public void Convert(ref DvInt8 src, ref DvInt2 dst)
		{
			dst = (DvInt2)src;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00050EA8 File Offset: 0x0004F0A8
		public void Convert(ref DvInt1 src, ref DvInt4 dst)
		{
			dst = src;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00050EBB File Offset: 0x0004F0BB
		public void Convert(ref DvInt2 src, ref DvInt4 dst)
		{
			dst = src;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00050ECE File Offset: 0x0004F0CE
		public void Convert(ref DvInt4 src, ref DvInt4 dst)
		{
			dst = src;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00050EDC File Offset: 0x0004F0DC
		public void Convert(ref DvInt8 src, ref DvInt4 dst)
		{
			dst = (DvInt4)src;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00050EEF File Offset: 0x0004F0EF
		public void Convert(ref DvInt1 src, ref DvInt8 dst)
		{
			dst = src;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00050F02 File Offset: 0x0004F102
		public void Convert(ref DvInt2 src, ref DvInt8 dst)
		{
			dst = src;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00050F15 File Offset: 0x0004F115
		public void Convert(ref DvInt4 src, ref DvInt8 dst)
		{
			dst = src;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00050F28 File Offset: 0x0004F128
		public void Convert(ref DvInt8 src, ref DvInt8 dst)
		{
			dst = src;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00050F36 File Offset: 0x0004F136
		public void Convert(ref DvTimeSpan src, ref DvInt8 dst)
		{
			dst = src.Ticks;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00050F44 File Offset: 0x0004F144
		public void Convert(ref DvDateTime src, ref DvInt8 dst)
		{
			dst = src.Ticks;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00050F54 File Offset: 0x0004F154
		public void Convert(ref DvDateTimeZone src, ref DvInt8 dst)
		{
			dst = src.UtcDateTime.Ticks;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00050F75 File Offset: 0x0004F175
		public void Convert(ref byte src, ref byte dst)
		{
			dst = src;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00050F7B File Offset: 0x0004F17B
		public void Convert(ref ushort src, ref byte dst)
		{
			dst = ((src <= 255) ? ((byte)src) : 0);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00050F8E File Offset: 0x0004F18E
		public void Convert(ref uint src, ref byte dst)
		{
			dst = ((src <= 255U) ? ((byte)src) : 0);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00050FA1 File Offset: 0x0004F1A1
		public void Convert(ref ulong src, ref byte dst)
		{
			dst = ((src <= 255UL) ? ((byte)src) : 0);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00050FB5 File Offset: 0x0004F1B5
		public void Convert(ref UInt128 src, ref byte dst)
		{
			dst = ((src.Hi == 0UL && src.Lo <= 255UL) ? ((byte)src.Lo) : 0);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00050FDB File Offset: 0x0004F1DB
		public void Convert(ref byte src, ref ushort dst)
		{
			dst = (ushort)src;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00050FE1 File Offset: 0x0004F1E1
		public void Convert(ref ushort src, ref ushort dst)
		{
			dst = src;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00050FE7 File Offset: 0x0004F1E7
		public void Convert(ref uint src, ref ushort dst)
		{
			dst = ((src <= 65535U) ? ((ushort)src) : 0);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00050FFA File Offset: 0x0004F1FA
		public void Convert(ref ulong src, ref ushort dst)
		{
			dst = ((src <= 65535UL) ? ((ushort)src) : 0);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0005100E File Offset: 0x0004F20E
		public void Convert(ref UInt128 src, ref ushort dst)
		{
			dst = ((src.Hi == 0UL && src.Lo <= 65535UL) ? ((ushort)src.Lo) : 0);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00051034 File Offset: 0x0004F234
		public void Convert(ref byte src, ref uint dst)
		{
			dst = (uint)src;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0005103A File Offset: 0x0004F23A
		public void Convert(ref ushort src, ref uint dst)
		{
			dst = (uint)src;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00051040 File Offset: 0x0004F240
		public void Convert(ref uint src, ref uint dst)
		{
			dst = src;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00051046 File Offset: 0x0004F246
		public void Convert(ref ulong src, ref uint dst)
		{
			dst = ((src <= (ulong)(-1)) ? ((uint)src) : 0U);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00051056 File Offset: 0x0004F256
		public void Convert(ref UInt128 src, ref uint dst)
		{
			dst = ((src.Hi == 0UL && src.Lo <= (ulong)(-1)) ? ((uint)src.Lo) : 0U);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00051078 File Offset: 0x0004F278
		public void Convert(ref byte src, ref ulong dst)
		{
			dst = (ulong)src;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0005107F File Offset: 0x0004F27F
		public void Convert(ref ushort src, ref ulong dst)
		{
			dst = (ulong)src;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00051086 File Offset: 0x0004F286
		public void Convert(ref uint src, ref ulong dst)
		{
			dst = (ulong)src;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005108D File Offset: 0x0004F28D
		public void Convert(ref ulong src, ref ulong dst)
		{
			dst = src;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00051093 File Offset: 0x0004F293
		public void Convert(ref UInt128 src, ref ulong dst)
		{
			dst = ((src.Hi == 0UL) ? src.Lo : 0UL);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000510AB File Offset: 0x0004F2AB
		public void Convert(ref byte src, ref UInt128 dst)
		{
			dst = new UInt128((ulong)src, 0UL);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000510BD File Offset: 0x0004F2BD
		public void Convert(ref ushort src, ref UInt128 dst)
		{
			dst = new UInt128((ulong)src, 0UL);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000510CF File Offset: 0x0004F2CF
		public void Convert(ref uint src, ref UInt128 dst)
		{
			dst = new UInt128((ulong)src, 0UL);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000510E1 File Offset: 0x0004F2E1
		public void Convert(ref ulong src, ref UInt128 dst)
		{
			dst = new UInt128(src, 0UL);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000510F2 File Offset: 0x0004F2F2
		public void Convert(ref UInt128 src, ref UInt128 dst)
		{
			dst = src;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00051100 File Offset: 0x0004F300
		public void Convert(ref DvInt1 src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00051110 File Offset: 0x0004F310
		public void Convert(ref DvInt2 src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00051120 File Offset: 0x0004F320
		public void Convert(ref DvInt4 src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00051130 File Offset: 0x0004F330
		public void Convert(ref DvInt8 src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00051140 File Offset: 0x0004F340
		public void Convert(ref byte src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00051147 File Offset: 0x0004F347
		public void Convert(ref ushort src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005114E File Offset: 0x0004F34E
		public void Convert(ref uint src, ref float dst)
		{
			dst = src;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00051156 File Offset: 0x0004F356
		public void Convert(ref ulong src, ref float dst)
		{
			dst = src;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005115E File Offset: 0x0004F35E
		public void Convert(ref DvTimeSpan src, ref float dst)
		{
			dst = (float)src.Ticks;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005116E File Offset: 0x0004F36E
		public void Convert(ref DvDateTime src, ref float dst)
		{
			dst = (float)src.Ticks;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00051180 File Offset: 0x0004F380
		public void Convert(ref DvDateTimeZone src, ref float dst)
		{
			dst = (float)src.UtcDateTime.Ticks;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000511A3 File Offset: 0x0004F3A3
		public void Convert(ref DvInt1 src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000511B3 File Offset: 0x0004F3B3
		public void Convert(ref DvInt2 src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000511C3 File Offset: 0x0004F3C3
		public void Convert(ref DvInt4 src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000511D3 File Offset: 0x0004F3D3
		public void Convert(ref DvInt8 src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000511E3 File Offset: 0x0004F3E3
		public void Convert(ref byte src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000511EA File Offset: 0x0004F3EA
		public void Convert(ref ushort src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000511F1 File Offset: 0x0004F3F1
		public void Convert(ref uint src, ref double dst)
		{
			dst = src;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000511F9 File Offset: 0x0004F3F9
		public void Convert(ref ulong src, ref double dst)
		{
			dst = src;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00051201 File Offset: 0x0004F401
		public void Convert(ref DvTimeSpan src, ref double dst)
		{
			dst = (double)src.Ticks;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00051211 File Offset: 0x0004F411
		public void Convert(ref DvDateTime src, ref double dst)
		{
			dst = (double)src.Ticks;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00051224 File Offset: 0x0004F424
		public void Convert(ref DvDateTimeZone src, ref double dst)
		{
			dst = (double)src.UtcDateTime.Ticks;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00051247 File Offset: 0x0004F447
		public void Convert(ref DvInt1 src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.Append(src.RawValue);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00051266 File Offset: 0x0004F466
		public void Convert(ref DvInt2 src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.Append(src.RawValue);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00051285 File Offset: 0x0004F485
		public void Convert(ref DvInt4 src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.Append(src.RawValue);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000512A4 File Offset: 0x0004F4A4
		public void Convert(ref DvInt8 src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.Append(src.RawValue);
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000512C3 File Offset: 0x0004F4C3
		public void Convert(ref byte src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst).Append(src);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000512D3 File Offset: 0x0004F4D3
		public void Convert(ref ushort src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst).Append(src);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000512E3 File Offset: 0x0004F4E3
		public void Convert(ref uint src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst).Append(src);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000512F3 File Offset: 0x0004F4F3
		public void Convert(ref ulong src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst).Append(src);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00051303 File Offset: 0x0004F503
		public void Convert(ref UInt128 src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			dst.AppendFormat("0x{0:x16}{1:x16}", src.Hi, src.Lo);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00051330 File Offset: 0x0004F530
		public void Convert(ref float src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!TypeUtils.IsNA(src))
			{
				dst.AppendFormat(CultureInfo.InvariantCulture, "{0:R}", new object[] { src });
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00051374 File Offset: 0x0004F574
		public void Convert(ref double src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!TypeUtils.IsNA(src))
			{
				dst.AppendFormat(CultureInfo.InvariantCulture, "{0:G17}", new object[] { src });
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000513B5 File Offset: 0x0004F5B5
		public void Convert(ref DvBool src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (src.IsFalse)
			{
				dst.Append("0");
				return;
			}
			if (src.IsTrue)
			{
				dst.Append("1");
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000513EC File Offset: 0x0004F5EC
		public void Convert(ref DvTimeSpan src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.AppendFormat("{0:c}", ((TimeSpan?)src).Value);
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00051430 File Offset: 0x0004F630
		public void Convert(ref DvDateTime src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.AppendFormat("{0:o}", ((DateTime?)src).Value);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00051474 File Offset: 0x0004F674
		public void Convert(ref DvDateTimeZone src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (!src.IsNA)
			{
				dst.AppendFormat("{0:o}", ((DateTimeOffset?)src).Value);
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000514B5 File Offset: 0x0004F6B5
		public void Convert(ref float src, ref float dst)
		{
			dst = src;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000514BB File Offset: 0x0004F6BB
		public void Convert(ref float src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000514C2 File Offset: 0x0004F6C2
		public void Convert(ref double src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000514C9 File Offset: 0x0004F6C9
		public void Convert(ref double src, ref double dst)
		{
			dst = src;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000514D0 File Offset: 0x0004F6D0
		public bool TryParse(ref DvText src, out byte dst)
		{
			ulong num;
			if (!this.TryParse(ref src, out num) || num > 255UL)
			{
				dst = 0;
				return false;
			}
			dst = (byte)num;
			return true;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000514FC File Offset: 0x0004F6FC
		public bool TryParse(ref DvText src, out ushort dst)
		{
			ulong num;
			if (!this.TryParse(ref src, out num) || num > 65535UL)
			{
				dst = 0;
				return false;
			}
			dst = (ushort)num;
			return true;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00051528 File Offset: 0x0004F728
		public bool TryParse(ref DvText src, out uint dst)
		{
			ulong num;
			if (!this.TryParse(ref src, out num) || num > (ulong)(-1))
			{
				dst = 0U;
				return false;
			}
			dst = (uint)num;
			return true;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00051550 File Offset: 0x0004F750
		public bool TryParse(ref DvText src, out ulong dst)
		{
			if (src.IsNA)
			{
				dst = 0UL;
				return false;
			}
			int num;
			int num2;
			string rawUnderlyingBufferInfo = src.GetRawUnderlyingBufferInfo(ref num, ref num2);
			return this.TryParseCore(rawUnderlyingBufferInfo, num, num2, out dst);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00051580 File Offset: 0x0004F780
		public bool TryParse(ref DvText src, out UInt128 dst)
		{
			if (src.Length != 34 || src[0] != '0' || (src[1] != 'x' && src[1] != 'X'))
			{
				dst = default(UInt128);
				return false;
			}
			int num;
			int num2;
			string rawUnderlyingBufferInfo = src.GetRawUnderlyingBufferInfo(ref num, ref num2);
			int num3 = num + 2;
			ulong num4 = 0UL;
			ulong num5 = 0UL;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					num5 <<= 4;
					char c = rawUnderlyingBufferInfo[num3++];
					if ('0' <= c && c <= '9')
					{
						num5 |= (ulong)(c - '0');
					}
					else if ('A' <= c && c <= 'F')
					{
						num5 |= (ulong)(c - 'A' + '\n');
					}
					else
					{
						if ('a' > c || c > 'f')
						{
							dst = default(UInt128);
							return false;
						}
						num5 |= (ulong)(c - 'a' + '\n');
					}
				}
				if (i == 0)
				{
					num4 = num5;
					num5 = 0UL;
				}
			}
			dst = new UInt128(num5, num4);
			return true;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00051688 File Offset: 0x0004F888
		private bool IsStdMissing(ref DvText src)
		{
			switch (src.Length)
			{
			case 1:
				return src[0] == '?';
			case 2:
			{
				char c;
				return ((c = src[0]) == 'N' || c == 'n') && ((c = src[1]) == 'A' || c == 'a');
			}
			case 3:
			{
				char c;
				if ((c = src[0]) != 'N' && c != 'n')
				{
					return false;
				}
				if ((c = src[1]) == '/')
				{
					if ((c = src[2]) != 'A' && c != 'a')
					{
						return false;
					}
				}
				else
				{
					if (c != 'a' && c != 'A')
					{
						return false;
					}
					if ((c = src[2]) != 'N' && c != 'n')
					{
						return false;
					}
				}
				return true;
			}
			default:
				return false;
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00051744 File Offset: 0x0004F944
		public bool TryParseKey(ref DvText src, ulong min, ulong max, out ulong dst)
		{
			if (!src.HasChars)
			{
				dst = 0UL;
				return true;
			}
			int num;
			int num2;
			string rawUnderlyingBufferInfo = src.GetRawUnderlyingBufferInfo(ref num, ref num2);
			ulong num3;
			if (!this.TryParseCore(rawUnderlyingBufferInfo, num, num2, out num3))
			{
				dst = 0UL;
				return this.IsStdMissing(ref src);
			}
			if (min > num3 || num3 > max)
			{
				dst = 0UL;
				return false;
			}
			dst = num3 - min + 1UL;
			return true;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000517A0 File Offset: 0x0004F9A0
		private bool TryParseCore(string text, int ich, int lim, out ulong dst)
		{
			ulong num = 0UL;
			while (ich < lim)
			{
				uint num2 = (uint)(text[ich++] - '0');
				if (num2 < 10U && (num & 16140901064495857664UL) == 0UL)
				{
					ulong num3 = num << 3;
					num = num3 + (num << 1) + (ulong)num2;
					if (num >= num3)
					{
						continue;
					}
				}
				dst = 0UL;
				return false;
			}
			dst = num;
			return true;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public bool TryParse(ref DvText src, out DvInt1 dst)
		{
			long num;
			bool flag = this.TryParseSigned(127L, ref src, out num);
			dst = (sbyte)num;
			return flag;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00051820 File Offset: 0x0004FA20
		public bool TryParse(ref DvText src, out DvInt2 dst)
		{
			long num;
			bool flag = this.TryParseSigned(32767L, ref src, out num);
			dst = (short)num;
			return flag;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0005184C File Offset: 0x0004FA4C
		public bool TryParse(ref DvText src, out DvInt4 dst)
		{
			long num;
			bool flag = this.TryParseSigned(2147483647L, ref src, out num);
			dst = (int)num;
			return flag;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00051878 File Offset: 0x0004FA78
		public bool TryParse(ref DvText src, out DvInt8 dst)
		{
			long num;
			bool flag = this.TryParseSigned(long.MaxValue, ref src, out num);
			dst = num;
			return flag;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000518A8 File Offset: 0x0004FAA8
		private bool TryParseNonNegative(string text, int ich, int lim, out long result)
		{
			long num = 0L;
			while (ich < lim)
			{
				uint num2 = (uint)(text[ich++] - '0');
				if (num2 < 10U && num < 1152921504606846976L)
				{
					num = (num << 3) + (num << 1) + (long)((ulong)num2);
					if (num >= 0L)
					{
						continue;
					}
				}
				result = 0L;
				return false;
			}
			result = num;
			return true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000518FC File Offset: 0x0004FAFC
		private bool TryParseSigned(long max, ref DvText span, out long result)
		{
			if (!span.HasChars)
			{
				if (span.IsNA)
				{
					result = -max - 1L;
				}
				else
				{
					result = 0L;
				}
				return true;
			}
			int num;
			int num2;
			string rawUnderlyingBufferInfo = span.GetRawUnderlyingBufferInfo(ref num, ref num2);
			if (span[0] == '-')
			{
				long num3;
				if (span.Length == 1 || !this.TryParseNonNegative(rawUnderlyingBufferInfo, num + 1, num2, out num3) || num3 > max)
				{
					result = -max - 1L;
					return false;
				}
				result = -num3;
				return true;
			}
			else
			{
				long num3;
				if (!this.TryParseNonNegative(rawUnderlyingBufferInfo, num, num2, out num3))
				{
					result = -max - 1L;
					return this.IsStdMissing(ref span);
				}
				if (num3 > max)
				{
					result = -max - 1L;
					return false;
				}
				result = num3;
				return true;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00051996 File Offset: 0x0004FB96
		public bool TryParse(ref DvText src, out float dst)
		{
			if (src.TryParse(ref dst))
			{
				return true;
			}
			dst = float.NaN;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000519B1 File Offset: 0x0004FBB1
		public bool TryParse(ref DvText src, out double dst)
		{
			if (src.TryParse(ref dst))
			{
				return true;
			}
			dst = double.NaN;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000519D0 File Offset: 0x0004FBD0
		public bool TryParse(ref DvText src, out DvTimeSpan dst)
		{
			if (!src.HasChars)
			{
				if (src.IsNA)
				{
					dst = DvTimeSpan.NA;
				}
				else
				{
					dst = default(DvTimeSpan);
				}
				return true;
			}
			TimeSpan timeSpan;
			if (TimeSpan.TryParse(src.ToString(), CultureInfo.InvariantCulture, out timeSpan))
			{
				dst = new DvTimeSpan(timeSpan);
				return true;
			}
			dst = DvTimeSpan.NA;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00051A40 File Offset: 0x0004FC40
		public bool TryParse(ref DvText src, out DvDateTime dst)
		{
			if (!src.HasChars)
			{
				if (src.IsNA)
				{
					dst = DvDateTime.NA;
				}
				else
				{
					dst = default(DvDateTime);
				}
				return true;
			}
			DateTime dateTime;
			if (DateTime.TryParse(src.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				dst = new DvDateTime(dateTime);
				return true;
			}
			dst = DvDateTime.NA;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		public bool TryParse(ref DvText src, out DvDateTimeZone dst)
		{
			if (!src.HasChars)
			{
				if (src.IsNA)
				{
					dst = DvDateTimeZone.NA;
				}
				else
				{
					dst = default(DvDateTimeZone);
				}
				return true;
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeOffset.TryParse(src.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
			{
				dst = new DvDateTimeZone(dateTimeOffset);
				return true;
			}
			dst = DvDateTimeZone.NA;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00051B20 File Offset: 0x0004FD20
		private DvInt1 ParseI1(ref DvText src)
		{
			long num;
			this.TryParseSigned(127L, ref src, out num);
			return (sbyte)num;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00051B44 File Offset: 0x0004FD44
		private DvInt2 ParseI2(ref DvText src)
		{
			long num;
			this.TryParseSigned(32767L, ref src, out num);
			return (short)num;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00051B68 File Offset: 0x0004FD68
		private DvInt4 ParseI4(ref DvText src)
		{
			long num;
			this.TryParseSigned(2147483647L, ref src, out num);
			return (int)num;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00051B8C File Offset: 0x0004FD8C
		private DvInt8 ParseI8(ref DvText src)
		{
			long num;
			this.TryParseSigned(long.MaxValue, ref src, out num);
			return num;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00051BB4 File Offset: 0x0004FDB4
		private byte ParseU1(ref DvText span)
		{
			ulong num;
			if (!this.TryParse(ref span, out num))
			{
				return 0;
			}
			if (num > 255UL)
			{
				return 0;
			}
			return (byte)num;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00051BDC File Offset: 0x0004FDDC
		private ushort ParseU2(ref DvText span)
		{
			ulong num;
			if (!this.TryParse(ref span, out num))
			{
				return 0;
			}
			if (num > 65535UL)
			{
				return 0;
			}
			return (ushort)num;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00051C04 File Offset: 0x0004FE04
		private uint ParseU4(ref DvText span)
		{
			ulong num;
			if (!this.TryParse(ref span, out num))
			{
				return 0U;
			}
			if (num > (ulong)(-1))
			{
				return 0U;
			}
			return (uint)num;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00051C28 File Offset: 0x0004FE28
		private ulong ParseU8(ref DvText span)
		{
			ulong num;
			if (!this.TryParse(ref span, out num))
			{
				return 0UL;
			}
			return num;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00051C44 File Offset: 0x0004FE44
		public bool TryParse(ref DvText src, out DvBool dst)
		{
			if (src.IsNA)
			{
				dst = DvBool.NA;
				return true;
			}
			switch (src.Length)
			{
			case 0:
				dst = DvBool.False;
				return true;
			case 1:
			{
				char c = src[0];
				if (c <= 'T')
				{
					if (c <= 'F')
					{
						switch (c)
						{
						case '+':
						case '1':
							break;
						case ',':
						case '.':
						case '/':
							goto IL_027C;
						case '-':
						case '0':
							goto IL_00CF;
						default:
							if (c != 'F')
							{
								goto IL_027C;
							}
							goto IL_00CF;
						}
					}
					else
					{
						if (c == 'N')
						{
							goto IL_00CF;
						}
						if (c != 'T')
						{
							break;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c != 'Y')
					{
						if (c != 'f')
						{
							break;
						}
						goto IL_00CF;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_00CF;
					}
					if (c != 't' && c != 'y')
					{
						break;
					}
				}
				dst = DvBool.True;
				return true;
				IL_00CF:
				dst = DvBool.False;
				return true;
			}
			case 2:
			{
				char c2 = src[0];
				switch (c2)
				{
				case '+':
					if (src[1] == '1')
					{
						dst = DvBool.True;
						return true;
					}
					break;
				case ',':
					break;
				case '-':
					if (src[1] == '1')
					{
						dst = DvBool.False;
						return true;
					}
					break;
				default:
				{
					char c3;
					if ((c2 == 'N' || c2 == 'n') && ((c3 = src[1]) == 'O' || c3 == 'o'))
					{
						dst = DvBool.False;
						return true;
					}
					break;
				}
				}
				break;
			}
			case 3:
			{
				char c4 = src[0];
				char c3;
				if ((c4 == 'Y' || c4 == 'y') && ((c3 = src[1]) == 'E' || c3 == 'e') && ((c3 = src[2]) == 'S' || c3 == 's'))
				{
					dst = DvBool.True;
					return true;
				}
				break;
			}
			case 4:
			{
				char c5 = src[0];
				char c3;
				if ((c5 == 'T' || c5 == 't') && ((c3 = src[1]) == 'R' || c3 == 'r') && ((c3 = src[2]) == 'U' || c3 == 'u') && ((c3 = src[3]) == 'E' || c3 == 'e'))
				{
					dst = DvBool.True;
					return true;
				}
				break;
			}
			case 5:
			{
				char c6 = src[0];
				char c3;
				if ((c6 == 'F' || c6 == 'f') && ((c3 = src[1]) == 'A' || c3 == 'a') && ((c3 = src[2]) == 'L' || c3 == 'l') && ((c3 = src[3]) == 'S' || c3 == 's') && ((c3 = src[4]) == 'E' || c3 == 'e'))
				{
					dst = DvBool.False;
					return true;
				}
				break;
			}
			}
			IL_027C:
			dst = DvBool.NA;
			return this.IsStdMissing(ref src);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00051EDF File Offset: 0x000500DF
		private bool TryParse(ref DvText src, out DvText dst)
		{
			dst = src;
			return true;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00051EEE File Offset: 0x000500EE
		public void Convert(ref DvText span, ref DvInt1 value)
		{
			value = this.ParseI1(ref span);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00051EFD File Offset: 0x000500FD
		public void Convert(ref DvText span, ref byte value)
		{
			value = this.ParseU1(ref span);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00051F08 File Offset: 0x00050108
		public void Convert(ref DvText span, ref DvInt2 value)
		{
			value = this.ParseI2(ref span);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00051F17 File Offset: 0x00050117
		public void Convert(ref DvText span, ref ushort value)
		{
			value = this.ParseU2(ref span);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00051F22 File Offset: 0x00050122
		public void Convert(ref DvText span, ref DvInt4 value)
		{
			value = this.ParseI4(ref span);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00051F31 File Offset: 0x00050131
		public void Convert(ref DvText span, ref uint value)
		{
			value = this.ParseU4(ref span);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00051F3C File Offset: 0x0005013C
		public void Convert(ref DvText span, ref DvInt8 value)
		{
			value = this.ParseI8(ref span);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00051F4B File Offset: 0x0005014B
		public void Convert(ref DvText span, ref ulong value)
		{
			value = this.ParseU8(ref span);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00051F56 File Offset: 0x00050156
		public void Convert(ref DvText span, ref UInt128 value)
		{
			this.TryParse(ref span, out value);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00051F61 File Offset: 0x00050161
		public void Convert(ref DvText span, ref float value)
		{
			if (span.TryParse(ref value))
			{
				return;
			}
			value = float.NaN;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00051F74 File Offset: 0x00050174
		public void Convert(ref DvText span, ref double value)
		{
			if (span.TryParse(ref value))
			{
				return;
			}
			value = double.NaN;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00051F8B File Offset: 0x0005018B
		public void Convert(ref DvText span, ref DvText value)
		{
			value = span;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00051F99 File Offset: 0x00050199
		public void Convert(ref DvText span, ref DvBool value)
		{
			this.TryParse(ref span, out value);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00051FA4 File Offset: 0x000501A4
		public void Convert(ref DvText src, ref StringBuilder dst)
		{
			Conversions.ClearDst(ref dst);
			if (src.HasChars)
			{
				src.AddToStringBuilder(dst);
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00051FBD File Offset: 0x000501BD
		public void Convert(ref DvText span, ref DvTimeSpan value)
		{
			this.TryParse(ref span, out value);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00051FC8 File Offset: 0x000501C8
		public void Convert(ref DvText span, ref DvDateTime value)
		{
			this.TryParse(ref span, out value);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00051FD3 File Offset: 0x000501D3
		public void Convert(ref DvText span, ref DvDateTimeZone value)
		{
			this.TryParse(ref span, out value);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00051FDE File Offset: 0x000501DE
		public void Convert(ref DvBool src, ref DvInt1 dst)
		{
			dst = (DvInt1)src;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00051FF1 File Offset: 0x000501F1
		public void Convert(ref DvBool src, ref DvInt2 dst)
		{
			dst = (DvInt2)src;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00052004 File Offset: 0x00050204
		public void Convert(ref DvBool src, ref DvInt4 dst)
		{
			dst = (DvInt4)src;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00052017 File Offset: 0x00050217
		public void Convert(ref DvBool src, ref DvInt8 dst)
		{
			dst = (DvInt8)src;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0005202A File Offset: 0x0005022A
		public void Convert(ref DvBool src, ref float dst)
		{
			dst = (float)src;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0005203A File Offset: 0x0005023A
		public void Convert(ref DvBool src, ref double dst)
		{
			dst = (double)src;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0005204A File Offset: 0x0005024A
		public void Convert(ref DvBool src, ref DvBool dst)
		{
			dst = src;
		}

		// Token: 0x04000808 RID: 2056
		private const DataKind _kindStringBuilder = 100;

		// Token: 0x04000809 RID: 2057
		private static volatile Conversions _instance;

		// Token: 0x0400080A RID: 2058
		private readonly Dictionary<Type, DataKind> _kinds;

		// Token: 0x0400080B RID: 2059
		private readonly Dictionary<int, Delegate> _delegatesStd;

		// Token: 0x0400080C RID: 2060
		private readonly Dictionary<int, Delegate> _delegatesAll;

		// Token: 0x0400080D RID: 2061
		private readonly Dictionary<DataKind, Delegate> _isNADelegates;

		// Token: 0x0400080E RID: 2062
		private readonly Dictionary<DataKind, Delegate> _hasNADelegates;

		// Token: 0x0400080F RID: 2063
		private readonly Dictionary<DataKind, Delegate> _isDefaultDelegates;

		// Token: 0x04000810 RID: 2064
		private readonly Dictionary<DataKind, Delegate> _hasZeroDelegates;

		// Token: 0x04000811 RID: 2065
		private readonly Dictionary<DataKind, Delegate> _getNADelegates;

		// Token: 0x04000812 RID: 2066
		private readonly Dictionary<DataKind, Delegate> _tryParseDelegates;
	}
}
