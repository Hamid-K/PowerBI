using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A6 RID: 422
	internal abstract class ExprNode : Node
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x000323F8 File Offset: 0x000305F8
		protected ExprNode(Token tok)
			: base(tok)
		{
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00032401 File Offset: 0x00030601
		public override ExprNode AsExpr
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00032404 File Offset: 0x00030604
		public override ExprNode TestExpr
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00032407 File Offset: 0x00030607
		public ExprType ExprType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0003240F File Offset: 0x0003060F
		public object ExprValue
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00032417 File Offset: 0x00030617
		private bool IsSimple(ExprTypeKind kind)
		{
			return this._type.Kind == kind;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00032427 File Offset: 0x00030627
		public bool HasType
		{
			get
			{
				return ExprTypeKind.Error < this._type.Kind && this._type.Kind < ExprTypeKind._Lim;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00032447 File Offset: 0x00030647
		public bool IsNone
		{
			get
			{
				return this._type.Kind == ExprTypeKind.None;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00032457 File Offset: 0x00030657
		public bool IsError
		{
			get
			{
				return this._type.Kind == ExprTypeKind.Error;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00032467 File Offset: 0x00030667
		public bool IsBool
		{
			get
			{
				return this.IsSimple(ExprTypeKind.BL);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00032470 File Offset: 0x00030670
		public bool IsNumber
		{
			get
			{
				return this.IsSimple(ExprTypeKind.I4) || this.IsSimple(ExprTypeKind.I8) || this.IsSimple(ExprTypeKind.R4) || this.IsSimple(ExprTypeKind.R8);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00032496 File Offset: 0x00030696
		public bool IsIx
		{
			get
			{
				return this.IsSimple(ExprTypeKind.I4) || this.IsSimple(ExprTypeKind.I8);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000324AA File Offset: 0x000306AA
		public bool IsI4
		{
			get
			{
				return this.IsSimple(ExprTypeKind.I4);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000324B3 File Offset: 0x000306B3
		public bool IsI8
		{
			get
			{
				return this.IsSimple(ExprTypeKind.I8);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000324BC File Offset: 0x000306BC
		public bool IsRx
		{
			get
			{
				return this.IsSimple(ExprTypeKind.R4) || this.IsSimple(ExprTypeKind.R8);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000324D0 File Offset: 0x000306D0
		public bool IsR4
		{
			get
			{
				return this.IsSimple(ExprTypeKind.R4);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000324D9 File Offset: 0x000306D9
		public bool IsR8
		{
			get
			{
				return this.IsSimple(ExprTypeKind.R8);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000324E2 File Offset: 0x000306E2
		public bool IsFloat
		{
			get
			{
				return this.IsSimple(ExprTypeKind.R4);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000324EB File Offset: 0x000306EB
		public bool IsTX
		{
			get
			{
				return this.IsSimple(ExprTypeKind.TX);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000324F4 File Offset: 0x000306F4
		public ExprTypeKind SrcKind
		{
			get
			{
				return this._kindSrc;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000324FC File Offset: 0x000306FC
		public bool NeedsConversion
		{
			get
			{
				return this._kindSrc != this._type.Kind;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00032514 File Offset: 0x00030714
		public void SetType(ExprType type)
		{
			this._type = type;
			this._kindSrc = type.Kind;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003252A File Offset: 0x0003072A
		public void SetType(ExprType type, object value)
		{
			this._type = type;
			this._kindSrc = type.Kind;
			this._value = value;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00032547 File Offset: 0x00030747
		public void SetValue(ExprNode expr)
		{
			this.SetType(expr._type);
			this._value = expr._value;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00032561 File Offset: 0x00030761
		public void SetValue(DvBool value)
		{
			this.SetType(ExprType.BL);
			this._value = value;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003257A File Offset: 0x0003077A
		public void SetValue(DvBool? value)
		{
			this.SetType(ExprType.BL);
			this._value = value;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00032593 File Offset: 0x00030793
		public void SetValue(DvInt4 value)
		{
			this.SetType(ExprType.I4);
			this._value = value;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000325AC File Offset: 0x000307AC
		public void SetValue(DvInt4? value)
		{
			this.SetType(ExprType.I4);
			this._value = value;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000325C5 File Offset: 0x000307C5
		public void SetValue(DvInt8 value)
		{
			this.SetType(ExprType.I8);
			this._value = value;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000325DE File Offset: 0x000307DE
		public void SetValue(DvInt8? value)
		{
			this.SetType(ExprType.I8);
			this._value = value;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000325F7 File Offset: 0x000307F7
		public void SetValue(float value)
		{
			this.SetType(ExprType.R4);
			this._value = value;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00032610 File Offset: 0x00030810
		public void SetValue(float? value)
		{
			this.SetType(ExprType.R4);
			this._value = value;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00032629 File Offset: 0x00030829
		public void SetValue(double value)
		{
			this.SetType(ExprType.R8);
			this._value = value;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00032642 File Offset: 0x00030842
		public void SetValue(double? value)
		{
			this.SetType(ExprType.R8);
			this._value = value;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003265B File Offset: 0x0003085B
		public void SetValue(DvText value)
		{
			this.SetType(ExprType.TX);
			this._value = value;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00032674 File Offset: 0x00030874
		public void SetValue(DvText? value)
		{
			this.SetType(ExprType.TX);
			this._value = value;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00032690 File Offset: 0x00030890
		public void Convert(ExprTypeKind kind)
		{
			if (kind == this._type.Kind)
			{
				return;
			}
			switch (kind)
			{
			case ExprTypeKind.I8:
				if (this._value != null)
				{
					this._value = (DvInt4)this._value;
				}
				break;
			case ExprTypeKind.R4:
				if (this._value != null)
				{
					this._value = (float)((DvInt4)this._value);
				}
				break;
			case ExprTypeKind.R8:
				if (this._value != null)
				{
					if (this._type.Kind == ExprTypeKind.I4)
					{
						this._value = (double)((DvInt4)this._value);
					}
					else if (this._type.Kind == ExprTypeKind.I8)
					{
						this._value = (double)((DvInt8)this._value);
					}
					else
					{
						this._value = (double)((float)this._value);
					}
				}
				break;
			}
			this._type = new ExprType(kind);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0003279E File Offset: 0x0003099E
		public bool TryGet(out DvBool? value)
		{
			if (this.IsBool)
			{
				value = (DvBool?)this._value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000327C3 File Offset: 0x000309C3
		public bool TryGet(out DvInt4? value)
		{
			if (this.IsI4)
			{
				value = (DvInt4?)this._value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000327E8 File Offset: 0x000309E8
		public bool TryGet(out DvInt8? value)
		{
			switch (this._type.Kind)
			{
			case ExprTypeKind.I4:
			case ExprTypeKind.I8:
				this.Convert(ExprTypeKind.I8);
				value = (DvInt8?)this._value;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00032834 File Offset: 0x00030A34
		public bool TryGet(out float? value)
		{
			switch (this._type.Kind)
			{
			case ExprTypeKind.I4:
			case ExprTypeKind.R4:
				this.Convert(ExprTypeKind.R4);
				value = (float?)this._value;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00032884 File Offset: 0x00030A84
		public bool TryGet(out double? value)
		{
			switch (this._type.Kind)
			{
			case ExprTypeKind.I4:
			case ExprTypeKind.I8:
			case ExprTypeKind.R4:
			case ExprTypeKind.R8:
				this.Convert(ExprTypeKind.R8);
				value = (double?)this._value;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000328D7 File Offset: 0x00030AD7
		public bool TryGet(out DvText? value)
		{
			if (this.IsTX)
			{
				value = (DvText?)this._value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x040004D7 RID: 1239
		private ExprType _type;

		// Token: 0x040004D8 RID: 1240
		private object _value;

		// Token: 0x040004D9 RID: 1241
		private ExprTypeKind _kindSrc;
	}
}
