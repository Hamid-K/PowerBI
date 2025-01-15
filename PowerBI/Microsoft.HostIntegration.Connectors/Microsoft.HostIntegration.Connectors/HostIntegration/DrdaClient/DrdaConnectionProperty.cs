using System;
using System.Globalization;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009EF RID: 2543
	internal class DrdaConnectionProperty
	{
		// Token: 0x06004EA9 RID: 20137 RVA: 0x0013C8E0 File Offset: 0x0013AAE0
		public DrdaConnectionProperty(string name, ConnectionKey key, object defaultValue, string[] synonyms, object[] allowableValues)
		{
			this._name = name;
			this._key = key;
			this._defaultValue = defaultValue;
			this._type = this._defaultValue.GetType();
			this._synonyms = ((synonyms != null) ? synonyms : new string[0]);
			object[] array2;
			if (allowableValues == null)
			{
				object[] array = new string[0];
				array2 = array;
			}
			else
			{
				array2 = allowableValues;
			}
			this._allowableValues = array2;
			this._minRestriction = 0;
			this._maxRestriction = 0;
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0013C951 File Offset: 0x0013AB51
		public DrdaConnectionProperty(string name, ConnectionKey key, int defaultValue, string[] synonyms, int minValue, int maxValue)
			: this(name, key, defaultValue, synonyms, null)
		{
			this._minRestriction = minValue;
			this._maxRestriction = maxValue;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0013C974 File Offset: 0x0013AB74
		public DrdaConnectionProperty(string name, ConnectionKey key, string defaultValue, string[] synonyms, object[] allowableValues, int maxLength)
			: this(name, key, defaultValue, synonyms, allowableValues)
		{
			this._maxRestriction = maxLength;
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x0013C98B File Offset: 0x0013AB8B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x0013C993 File Offset: 0x0013AB93
		public string[] Synonyms
		{
			get
			{
				return this._synonyms;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0013C99B File Offset: 0x0013AB9B
		public ConnectionKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x0013C9A3 File Offset: 0x0013ABA3
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x0013C9AB File Offset: 0x0013ABAB
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x0013C9B3 File Offset: 0x0013ABB3
		public object[] AllowableValues
		{
			get
			{
				return this._allowableValues;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x0013C9BB File Offset: 0x0013ABBB
		public int MinRestriction
		{
			get
			{
				return this._minRestriction;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x0013C9C3 File Offset: 0x0013ABC3
		public int MaxRestriction
		{
			get
			{
				return this._maxRestriction;
			}
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0013C9CC File Offset: 0x0013ABCC
		public void ValidateValue(object value)
		{
			if (this.Type != value.GetType())
			{
				throw new Exception("Value is of wrong type");
			}
			if (this.Type == typeof(int) && ((int)value < this.MinRestriction || (int)value > this.MaxRestriction))
			{
				throw new Exception("Value is out of range");
			}
			if (this.Type == typeof(string) && ((string)value).Length > this.MaxRestriction)
			{
				throw DrdaException.InvalidConnectionOptionLength(this.Name, this.MaxRestriction);
			}
			if (this._allowableValues.Length != 0)
			{
				bool flag = false;
				int num = 0;
				while (num < this._allowableValues.Length && !flag)
				{
					if (this._allowableValues[num].Equals(value))
					{
						flag = true;
					}
					num++;
				}
				if (!flag)
				{
					throw new Exception("Value is not allowed");
				}
			}
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x0013CAB2 File Offset: 0x0013ACB2
		public string ConvertToString(object val)
		{
			return val.ToString();
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0013CABC File Offset: 0x0013ACBC
		public object ConvertFromString(string val)
		{
			if (this.Type == typeof(string))
			{
				return val;
			}
			if (this.Type == typeof(int))
			{
				return int.Parse(val, CultureInfo.InvariantCulture);
			}
			if (this.Type == typeof(bool))
			{
				return bool.Parse(val);
			}
			for (int i = 0; i < this._allowableValues.Length; i++)
			{
				if (this._allowableValues[i].ToString().Equals(val))
				{
					return this._allowableValues[i];
				}
			}
			throw new Exception("value could not be converted");
		}

		// Token: 0x04003EF3 RID: 16115
		private string _name;

		// Token: 0x04003EF4 RID: 16116
		private string[] _synonyms;

		// Token: 0x04003EF5 RID: 16117
		private ConnectionKey _key;

		// Token: 0x04003EF6 RID: 16118
		private Type _type;

		// Token: 0x04003EF7 RID: 16119
		private object _defaultValue;

		// Token: 0x04003EF8 RID: 16120
		private object[] _allowableValues;

		// Token: 0x04003EF9 RID: 16121
		private int _minRestriction;

		// Token: 0x04003EFA RID: 16122
		private int _maxRestriction;
	}
}
