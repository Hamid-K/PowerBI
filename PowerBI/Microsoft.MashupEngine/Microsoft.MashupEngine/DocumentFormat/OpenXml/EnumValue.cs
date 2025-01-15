using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200213D RID: 8509
	[DebuggerDisplay("{InnerText}")]
	public class EnumValue<T> : OpenXmlSimpleType where T : struct
	{
		// Token: 0x0600D33E RID: 54078 RVA: 0x0029D744 File Offset: 0x0029B944
		public EnumValue()
		{
		}

		// Token: 0x0600D33F RID: 54079 RVA: 0x0029ED8B File Offset: 0x0029CF8B
		public EnumValue(T value)
		{
			if (!Enum.IsDefined(typeof(T), value))
			{
				throw new ArgumentOutOfRangeException("value", ExceptionMessages.InvalidEnumValue);
			}
			this.Value = value;
		}

		// Token: 0x0600D340 RID: 54080 RVA: 0x0029EDC1 File Offset: 0x0029CFC1
		public EnumValue(EnumValue<T> source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this._enumValue = source._enumValue;
		}

		// Token: 0x17003305 RID: 13061
		// (get) Token: 0x0600D341 RID: 54081 RVA: 0x0029EDE4 File Offset: 0x0029CFE4
		public override bool HasValue
		{
			get
			{
				if (this._enumValue == null && base.TextValue != null)
				{
					this.TryParse();
				}
				return this._enumValue != null;
			}
		}

		// Token: 0x17003306 RID: 13062
		// (get) Token: 0x0600D342 RID: 54082 RVA: 0x0029EE0D File Offset: 0x0029D00D
		// (set) Token: 0x0600D343 RID: 54083 RVA: 0x0029EE3A File Offset: 0x0029D03A
		public T Value
		{
			get
			{
				if (this._enumValue == null && !string.IsNullOrEmpty(base.TextValue))
				{
					this.Parse();
				}
				return this._enumValue.Value;
			}
			set
			{
				if (!Enum.IsDefined(typeof(T), value))
				{
					throw new ArgumentOutOfRangeException("value", ExceptionMessages.InvalidEnumValue);
				}
				this._enumValue = new T?(value);
				base.TextValue = null;
			}
		}

		// Token: 0x17003307 RID: 13063
		// (get) Token: 0x0600D344 RID: 54084 RVA: 0x0029EE76 File Offset: 0x0029D076
		// (set) Token: 0x0600D345 RID: 54085 RVA: 0x0029EEA9 File Offset: 0x0029D0A9
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && this._enumValue != null)
				{
					base.TextValue = EnumValue<T>.ToString(this._enumValue.Value);
				}
				return base.TextValue;
			}
			set
			{
				base.TextValue = value;
				this._enumValue = null;
			}
		}

		// Token: 0x0600D346 RID: 54086 RVA: 0x0029EEBE File Offset: 0x0029D0BE
		public static implicit operator T(EnumValue<T> xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D347 RID: 54087 RVA: 0x0029EED4 File Offset: 0x0029D0D4
		public static implicit operator EnumValue<T>(T value)
		{
			return new EnumValue<T>(value);
		}

		// Token: 0x0600D348 RID: 54088 RVA: 0x0029D726 File Offset: 0x0029B926
		public static implicit operator string(EnumValue<T> value)
		{
			if (value == null)
			{
				return null;
			}
			return value.InnerText;
		}

		// Token: 0x0600D349 RID: 54089 RVA: 0x0029EEDC File Offset: 0x0029D0DC
		internal override OpenXmlSimpleType CloneImp()
		{
			return new EnumValue<T>(this);
		}

		// Token: 0x0600D34A RID: 54090 RVA: 0x0029EEE4 File Offset: 0x0029D0E4
		internal override void Parse()
		{
			this._enumValue = new T?(EnumValue<T>.GetEnumValue(base.TextValue));
		}

		// Token: 0x0600D34B RID: 54091 RVA: 0x0029EEFC File Offset: 0x0029D0FC
		internal override bool TryParse()
		{
			this._enumValue = null;
			T t;
			if (EnumValue<T>.TryGetEnumValue(base.TextValue, out t))
			{
				this._enumValue = new T?(t);
				return true;
			}
			return false;
		}

		// Token: 0x0600D34C RID: 54092 RVA: 0x0029EF34 File Offset: 0x0029D134
		internal static string ToString(T enumVal)
		{
			FieldInfo field = enumVal.GetType().GetField(enumVal.ToString());
			EnumStringAttribute enumStringAttribute = Attribute.GetCustomAttribute(field, typeof(EnumStringAttribute)) as EnumStringAttribute;
			if (enumStringAttribute != null)
			{
				return enumStringAttribute.Value;
			}
			return string.Empty;
		}

		// Token: 0x0600D34D RID: 54093 RVA: 0x0029EF88 File Offset: 0x0029D188
		internal static bool TryGetEnumValue(string stringVal, out T result)
		{
			if (EnumValue<T>.parseCount > 11)
			{
				if (EnumValue<T>.stringToValueLookupTable == null)
				{
					Dictionary<string, T> dictionary = new Dictionary<string, T>();
					Array values = Enum.GetValues(typeof(T));
					foreach (object obj in values)
					{
						T t = (T)((object)obj);
						FieldInfo field = t.GetType().GetField(t.ToString());
						EnumStringAttribute enumStringAttribute = Attribute.GetCustomAttribute(field, typeof(EnumStringAttribute)) as EnumStringAttribute;
						dictionary.Add(enumStringAttribute.Value, t);
					}
					EnumValue<T>.stringToValueLookupTable = dictionary;
				}
				return EnumValue<T>.stringToValueLookupTable.TryGetValue(stringVal, out result);
			}
			EnumValue<T>.parseCount++;
			Array values2 = Enum.GetValues(typeof(T));
			foreach (object obj2 in values2)
			{
				T t2 = (T)((object)obj2);
				FieldInfo field2 = t2.GetType().GetField(t2.ToString());
				EnumStringAttribute enumStringAttribute2 = Attribute.GetCustomAttribute(field2, typeof(EnumStringAttribute)) as EnumStringAttribute;
				if (enumStringAttribute2 != null && enumStringAttribute2.Value == stringVal)
				{
					result = t2;
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600D34E RID: 54094 RVA: 0x0029F130 File Offset: 0x0029D330
		internal static T GetEnumValue(string stringVal)
		{
			T t;
			if (EnumValue<T>.TryGetEnumValue(stringVal, out t))
			{
				return t;
			}
			throw new FormatException(ExceptionMessages.TextIsInvalidEnumValue);
		}

		// Token: 0x0600D34F RID: 54095 RVA: 0x0029F154 File Offset: 0x0029D354
		internal override bool IsInVersion(FileFormatVersions fileFormat)
		{
			if (!EnumValue<T>.fileFormatInformationLoaded && EnumValue<T>.fileFormatInformations == null)
			{
				Dictionary<T, FileFormatVersions> dictionary = null;
				Array values = Enum.GetValues(typeof(T));
				foreach (object obj in values)
				{
					T t = (T)((object)obj);
					T value = this.Value;
					FieldInfo field = value.GetType().GetField(t.ToString());
					OfficeAvailabilityAttribute officeAvailabilityAttribute = Attribute.GetCustomAttribute(field, typeof(OfficeAvailabilityAttribute)) as OfficeAvailabilityAttribute;
					if (officeAvailabilityAttribute != null)
					{
						if (dictionary == null)
						{
							dictionary = new Dictionary<T, FileFormatVersions>();
						}
						dictionary.Add(t, officeAvailabilityAttribute.OfficeVersion);
					}
				}
				lock (EnumValue<T>.lockroot)
				{
					EnumValue<T>.fileFormatInformations = dictionary;
					EnumValue<T>.fileFormatInformationLoaded = true;
				}
			}
			FileFormatVersions fileFormatVersions;
			return EnumValue<T>.fileFormatInformations == null || !EnumValue<T>.fileFormatInformations.TryGetValue(this.Value, out fileFormatVersions) || fileFormatVersions.Includes(fileFormat);
		}

		// Token: 0x0400698D RID: 27021
		private const int Threshold = 11;

		// Token: 0x0400698E RID: 27022
		private T? _enumValue;

		// Token: 0x0400698F RID: 27023
		private static int parseCount;

		// Token: 0x04006990 RID: 27024
		private static Dictionary<string, T> stringToValueLookupTable;

		// Token: 0x04006991 RID: 27025
		private static Dictionary<T, FileFormatVersions> fileFormatInformations;

		// Token: 0x04006992 RID: 27026
		private static bool fileFormatInformationLoaded;

		// Token: 0x04006993 RID: 27027
		private static object lockroot = new object();
	}
}
