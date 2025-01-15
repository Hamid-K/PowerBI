using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002E3 RID: 739
	[Serializable]
	internal struct LocalizedString : ISerializable, ILocalizedString, IFormattable, IEquatable<LocalizedString>
	{
		// Token: 0x06001A03 RID: 6659 RVA: 0x00046A96 File Offset: 0x00045A96
		public static bool operator ==(LocalizedString s1, LocalizedString s2)
		{
			return s1.Equals(s2);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00046AA0 File Offset: 0x00045AA0
		public static bool operator !=(LocalizedString s1, LocalizedString s2)
		{
			return !s1.Equals(s2);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00046AAD File Offset: 0x00045AAD
		public static implicit operator string(LocalizedString value)
		{
			return value.ToString();
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00046ABC File Offset: 0x00045ABC
		public static LocalizedString Join(object separator, object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return LocalizedString.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			object[] array = new object[value.Length + 1];
			array[0] = separator;
			Array.Copy(value, 0, array, 1, value.Length);
			StringBuilder stringBuilder = new StringBuilder(6 * value.Length);
			stringBuilder.Append("{");
			for (int i = 1; i < value.Length; i++)
			{
				stringBuilder.Append(i);
				stringBuilder.Append("}{0}{");
			}
			stringBuilder.Append(value.Length + "}");
			return new LocalizedString(stringBuilder.ToString(), array);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00046B5A File Offset: 0x00045B5A
		public LocalizedString(string id, ExchangeResourceManager resourceManager, params object[] inserts)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (resourceManager == null)
			{
				throw new ArgumentNullException("resourceManager");
			}
			this.Id = id;
			this.ResourceManager = resourceManager;
			this.Inserts = ((inserts != null && inserts.Length > 0) ? inserts : null);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00046B99 File Offset: 0x00045B99
		public LocalizedString(string value)
		{
			this.Id = value;
			this.Inserts = null;
			this.ResourceManager = null;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00046BB0 File Offset: 0x00045BB0
		private LocalizedString(string format, object[] inserts)
		{
			this.Id = format;
			this.Inserts = inserts;
			this.ResourceManager = null;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00046BC8 File Offset: 0x00045BC8
		private LocalizedString(SerializationInfo info, StreamingContext context)
		{
			this.Inserts = (object[])info.GetValue("inserts", typeof(object[]));
			this.ResourceManager = null;
			this.Id = null;
			try
			{
				string @string = info.GetString("baseName");
				string string2 = info.GetString("assemblyName");
				Assembly assembly = Assembly.Load(string2);
				this.ResourceManager = ExchangeResourceManager.GetResourceManager(@string, assembly);
				this.Id = info.GetString("id");
				if (this.ResourceManager.GetString(this.Id) == null)
				{
					this.ResourceManager = null;
				}
			}
			catch (SerializationException)
			{
				this.ResourceManager = null;
			}
			catch (FileNotFoundException)
			{
				this.ResourceManager = null;
			}
			catch (MissingManifestResourceException)
			{
				this.ResourceManager = null;
			}
			if (this.ResourceManager == null)
			{
				this.Id = info.GetString("fallback");
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00046CBC File Offset: 0x00045CBC
		[SecurityPermission(6, Flags = 128)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			object[] array = null;
			if (this.Inserts != null && this.Inserts.Length > 0)
			{
				array = new object[this.Inserts.Length];
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					object obj = this.Inserts[i];
					if (obj != null)
					{
						if (obj is ILocalizedString)
						{
							obj = ((ILocalizedString)obj).LocalizedString;
						}
						else if (!obj.GetType().IsSerializable && !(obj is ISerializable))
						{
							object obj2 = LocalizedString.TranslateObject(obj, CultureInfo.InvariantCulture);
							if (obj2 == obj)
							{
								obj = obj.ToString();
							}
							else
							{
								obj = obj2;
							}
						}
					}
					array[i] = obj;
				}
			}
			info.AddValue("inserts", array);
			if (this.ResourceManager != null)
			{
				info.AddValue("baseName", this.ResourceManager.BaseName);
				info.AddValue("assemblyName", this.ResourceManager.AssemblyName);
				info.AddValue("id", this.Id);
				info.AddValue("fallback", this.ResourceManager.GetString(this.Id, CultureInfo.InvariantCulture));
				return;
			}
			info.AddValue("fallback", this.Id);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00046DE1 File Offset: 0x00045DE1
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00046DF5 File Offset: 0x00045DF5
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00046E0C File Offset: 0x00045E0C
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			format = ((this.ResourceManager != null) ? this.ResourceManager.GetString(this.Id, formatProvider as CultureInfo) : this.Id);
			if (this.Inserts != null && this.Inserts.Length > 0)
			{
				object[] array = new object[this.Inserts.Length];
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					object obj = this.Inserts[i];
					if (obj is ILocalizedString)
					{
						obj = ((ILocalizedString)obj).LocalizedString;
					}
					else
					{
						obj = LocalizedString.TranslateObject(obj, formatProvider);
					}
					array[i] = obj;
				}
				return string.Format(formatProvider, format, array);
			}
			return format;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00046EBE File Offset: 0x00045EBE
		LocalizedString ILocalizedString.LocalizedString
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00046EC6 File Offset: 0x00045EC6
		public bool IsEmpty
		{
			get
			{
				return null == this.Id;
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00046ED4 File Offset: 0x00045ED4
		public override int GetHashCode()
		{
			int num = ((this.Id != null) ? this.Id.GetHashCode() : 0);
			int num2 = ((this.ResourceManager != null) ? this.ResourceManager.GetHashCode() : 0);
			int num3 = num ^ num2;
			if (this.Inserts != null)
			{
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					num3 = num3 ^ i ^ ((this.Inserts[i] != null) ? this.Inserts[i].GetHashCode() : 0);
				}
			}
			return num3;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00046F4E File Offset: 0x00045F4E
		public override bool Equals(object obj)
		{
			return obj is LocalizedString && this.Equals((LocalizedString)obj);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00046F68 File Offset: 0x00045F68
		public bool Equals(LocalizedString obj)
		{
			if (!string.Equals(this.Id, obj.Id, 5))
			{
				return false;
			}
			if ((null != this.ResourceManager) ^ (null != obj.ResourceManager))
			{
				return false;
			}
			if (this.ResourceManager != null && !this.ResourceManager.Equals(obj.ResourceManager))
			{
				return false;
			}
			if ((null != this.Inserts) ^ (null != obj.Inserts))
			{
				return false;
			}
			if (this.Inserts != null && obj.Inserts != null)
			{
				if (this.Inserts.Length != obj.Inserts.Length)
				{
					return false;
				}
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					if ((null != this.Inserts[i]) ^ (null != obj.Inserts[i]))
					{
						return false;
					}
					if (this.Inserts[i] != null && obj.Inserts[i] != null && !this.Inserts[i].Equals(obj.Inserts[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00047074 File Offset: 0x00046074
		private static object TranslateObject(object badObject, IFormatProvider formatProvider)
		{
			Exception ex = badObject as Exception;
			if (ex != null)
			{
				return ex.Message;
			}
			return badObject;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00047094 File Offset: 0x00046094
		public int BaseId
		{
			get
			{
				string text = ((this.ResourceManager != null) ? this.ResourceManager.BaseName : string.Empty) + this.Id;
				return text.GetHashCode();
			}
		}

		// Token: 0x0400140F RID: 5135
		private readonly string Id;

		// Token: 0x04001410 RID: 5136
		private readonly object[] Inserts;

		// Token: 0x04001411 RID: 5137
		private readonly ExchangeResourceManager ResourceManager;

		// Token: 0x04001412 RID: 5138
		public static readonly LocalizedString Empty = default(LocalizedString);
	}
}
