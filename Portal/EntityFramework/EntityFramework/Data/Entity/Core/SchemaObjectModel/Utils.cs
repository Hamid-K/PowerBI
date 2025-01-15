using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000325 RID: 805
	internal static class Utils
	{
		// Token: 0x06002658 RID: 9816 RVA: 0x0006E692 File Offset: 0x0006C892
		internal static void ExtractNamespaceAndName(string qualifiedTypeName, out string namespaceName, out string name)
		{
			Utils.GetBeforeAndAfterLastPeriod(qualifiedTypeName, out namespaceName, out name);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0006E69C File Offset: 0x0006C89C
		internal static string ExtractTypeName(string qualifiedTypeName)
		{
			return Utils.GetEverythingAfterLastPeriod(qualifiedTypeName);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0006E6A4 File Offset: 0x0006C8A4
		private static void GetBeforeAndAfterLastPeriod(string qualifiedTypeName, out string before, out string after)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				before = null;
				after = qualifiedTypeName;
				return;
			}
			before = qualifiedTypeName.Substring(0, num);
			after = qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0006E6DC File Offset: 0x0006C8DC
		internal static string GetEverythingBeforeLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return null;
			}
			return qualifiedTypeName.Substring(0, num);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0006E700 File Offset: 0x0006C900
		private static string GetEverythingAfterLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return qualifiedTypeName;
			}
			return qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0006E725 File Offset: 0x0006C925
		public static bool GetString(Schema schema, XmlReader reader, out string value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = null;
				return false;
			}
			value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(value, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0006E765 File Offset: 0x0006C965
		public static bool GetDottedName(Schema schema, XmlReader reader, out string name)
		{
			return Utils.GetString(schema, reader, out name) && Utils.ValidateDottedName(schema, reader, name);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0006E77C File Offset: 0x0006C97C
		internal static bool ValidateDottedName(Schema schema, XmlReader reader, string name)
		{
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				string[] array = name.Split(new char[] { '.' });
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].IsValidUndottedName())
					{
						schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0006E7D4 File Offset: 0x0006C9D4
		public static bool GetUndottedName(Schema schema, XmlReader reader, out string name)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				name = null;
				return false;
			}
			name = reader.Value;
			if (string.IsNullOrEmpty(name))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.EmptyName(reader.Name));
				return false;
			}
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel && !name.IsValidUndottedName())
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0006E848 File Offset: 0x0006CA48
		public static bool GetBool(Schema schema, XmlReader reader, out bool value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = true;
				return false;
			}
			try
			{
				value = reader.ReadContentAsBoolean();
				return true;
			}
			catch (XmlException)
			{
				schema.AddError(ErrorCode.BoolValueExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			}
			value = true;
			return false;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0006E8AC File Offset: 0x0006CAAC
		public static bool GetInt(Schema schema, XmlReader reader, out int value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = 0;
				return false;
			}
			string value2 = reader.Value;
			value = int.MinValue;
			if (int.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.IntegerExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0006E908 File Offset: 0x0006CB08
		public static bool GetByte(Schema schema, XmlReader reader, out byte value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = 0;
				return false;
			}
			string value2 = reader.Value;
			value = 0;
			if (byte.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.ByteValueExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0006E95D File Offset: 0x0006CB5D
		public static int CompareNames(string lhsName, string rhsName)
		{
			return string.Compare(lhsName, rhsName, StringComparison.Ordinal);
		}
	}
}
