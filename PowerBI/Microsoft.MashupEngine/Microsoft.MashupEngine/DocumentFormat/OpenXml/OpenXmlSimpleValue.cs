using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002129 RID: 8489
	[DebuggerDisplay("{InnerText}")]
	internal abstract class OpenXmlSimpleValue<T> : OpenXmlSimpleType where T : struct
	{
		// Token: 0x170032EB RID: 13035
		// (get) Token: 0x0600D266 RID: 53862 RVA: 0x0029D733 File Offset: 0x0029B933
		// (set) Token: 0x0600D267 RID: 53863 RVA: 0x0029D73B File Offset: 0x0029B93B
		internal T? InnerValue
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600D268 RID: 53864 RVA: 0x0029D744 File Offset: 0x0029B944
		protected OpenXmlSimpleValue()
		{
		}

		// Token: 0x0600D269 RID: 53865 RVA: 0x0029D74C File Offset: 0x0029B94C
		protected OpenXmlSimpleValue(T value)
		{
			this.Value = value;
		}

		// Token: 0x0600D26A RID: 53866 RVA: 0x0029D75B File Offset: 0x0029B95B
		protected OpenXmlSimpleValue(OpenXmlSimpleValue<T> source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.InnerText = source.InnerText;
		}

		// Token: 0x170032EC RID: 13036
		// (get) Token: 0x0600D26B RID: 53867 RVA: 0x0029D77E File Offset: 0x0029B97E
		public override bool HasValue
		{
			get
			{
				if (this._value == null && !string.IsNullOrEmpty(base.TextValue))
				{
					this.TryParse();
				}
				return this._value != null;
			}
		}

		// Token: 0x170032ED RID: 13037
		// (get) Token: 0x0600D26C RID: 53868 RVA: 0x0029D7AC File Offset: 0x0029B9AC
		// (set) Token: 0x0600D26D RID: 53869 RVA: 0x0029D7D9 File Offset: 0x0029B9D9
		public T Value
		{
			get
			{
				if (this._value == null && !string.IsNullOrEmpty(base.TextValue))
				{
					this.Parse();
				}
				return this._value.Value;
			}
			set
			{
				this._value = new T?(value);
				base.TextValue = null;
			}
		}

		// Token: 0x170032EE RID: 13038
		// (get) Token: 0x0600D26E RID: 53870 RVA: 0x000091AE File Offset: 0x000073AE
		// (set) Token: 0x0600D26F RID: 53871 RVA: 0x0029D7EE File Offset: 0x0029B9EE
		public override string InnerText
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				base.TextValue = value;
				this._value = null;
			}
		}

		// Token: 0x0600D270 RID: 53872 RVA: 0x0029D803 File Offset: 0x0029BA03
		public static implicit operator T(OpenXmlSimpleValue<T> xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x04006984 RID: 27012
		private T? _value;
	}
}
