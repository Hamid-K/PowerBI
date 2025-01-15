using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200000F RID: 15
	public sealed class MashupPermission : IEquatable<MashupPermission>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004913 File Offset: 0x00002B13
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000491B File Offset: 0x00002B1B
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004923 File Offset: 0x00002B23
		public IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000492B File Offset: 0x00002B2B
		public MashupPermission(string kind, string value)
			: this(kind, value, null)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004938 File Offset: 0x00002B38
		public MashupPermission(string kind, string value, IDictionary<string, object> properties)
		{
			if (kind == null)
			{
				throw new ArgumentNullException("kind");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.kind = kind;
			this.value = value;
			this.properties = properties ?? new Dictionary<string, object>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004988 File Offset: 0x00002B88
		public bool Equals(MashupPermission other)
		{
			if (other == null || this.Kind != other.Kind || this.Value != other.Value)
			{
				return false;
			}
			if (this.Properties.Count != other.Properties.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.Properties)
			{
				object obj;
				if (!other.Properties.TryGetValue(keyValuePair.Key, out obj) || !MashupPermission.PropertyComparer.Instance.Equals(keyValuePair.Value, obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004A44 File Offset: 0x00002C44
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MashupPermission);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A54 File Offset: 0x00002C54
		public override int GetHashCode()
		{
			int num = this.properties.Select((KeyValuePair<string, object> kvp) => kvp.Key.GetHashCode() ^ MashupPermission.PropertyComparer.Instance.GetHashCode(kvp.Value)).Aggregate(0, (int left, int right) => left ^ right);
			return this.Kind.GetHashCode() ^ this.Value.GetHashCode() ^ num;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004ACA File Offset: 0x00002CCA
		internal string SerializedProperties
		{
			get
			{
				return new JavaScriptSerializer
				{
					RecursionLimit = 10,
					MaxJsonLength = 1048576
				}.Serialize(this.Properties);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004AEF File Offset: 0x00002CEF
		internal static IDictionary<string, object> DeserializeProperties(string data)
		{
			return new JavaScriptSerializer
			{
				RecursionLimit = 10,
				MaxJsonLength = 1048576
			}.Deserialize<IDictionary<string, object>>(data);
		}

		// Token: 0x0400004C RID: 76
		private const int RecursionLimit = 10;

		// Token: 0x0400004D RID: 77
		private const int MaxJsonStringLength = 1048576;

		// Token: 0x0400004E RID: 78
		internal static string Parameters = "Parameters";

		// Token: 0x0400004F RID: 79
		private readonly string kind;

		// Token: 0x04000050 RID: 80
		private readonly string value;

		// Token: 0x04000051 RID: 81
		private readonly IDictionary<string, object> properties;

		// Token: 0x0200005A RID: 90
		private class PropertyComparer : IEqualityComparer<object>
		{
			// Token: 0x060003FC RID: 1020 RVA: 0x0000F280 File Offset: 0x0000D480
			public bool Equals(object left, object right)
			{
				if (left == right)
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				IList list = left as IList;
				IList list2 = right as IList;
				if (list == null || list2 == null)
				{
					return left.Equals(right);
				}
				if (list.Count != list2.Count)
				{
					return false;
				}
				IEnumerable<object> enumerable = list.Cast<object>();
				IEnumerable<object> enumerable2 = list2.Cast<object>();
				return enumerable.SequenceEqual(enumerable2, this);
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x0000F2DC File Offset: 0x0000D4DC
			public int GetHashCode(object obj)
			{
				if (obj == null)
				{
					return 0;
				}
				if (obj is IList)
				{
					return (from object val in (IList)obj
						select this.GetHashCode(val)).Aggregate(616, (int left, int right) => (left * 7) ^ right);
				}
				return obj.GetHashCode();
			}

			// Token: 0x040001F4 RID: 500
			public static MashupPermission.PropertyComparer Instance = new MashupPermission.PropertyComparer();
		}
	}
}
