using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200095F RID: 2399
	public static class Utilities
	{
		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000E6102 File Offset: 0x000E4302
		public static bool IsNet46Installed
		{
			get
			{
				if (Utilities.isNet46Installed == null)
				{
					Utilities.isNet46Installed = new bool?(FxVersionDetector.InstalledFxBuild >= 393295);
				}
				return Utilities.isNet46Installed.Value;
			}
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x000E6134 File Offset: 0x000E4334
		public static bool HasFlag(this Enum enumVariable, Enum enumValue)
		{
			if (enumVariable == null || enumValue == null)
			{
				return false;
			}
			ulong num = Convert.ToUInt64(enumValue, CultureInfo.InvariantCulture);
			return (Convert.ToUInt64(enumVariable, CultureInfo.InvariantCulture) & num) == num;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x000E6168 File Offset: 0x000E4368
		public static T GetValue<T>(this IDictionary<MqFunctionOption, object> options, MqFunctionOption field, T defaultValue)
		{
			if (options == null)
			{
				return defaultValue;
			}
			object obj;
			if (options.TryGetValue(field, out obj))
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x000E6190 File Offset: 0x000E4390
		public static byte[] BytesFromString(string value, int? fixedLength = null, int? codedCharSetId = null)
		{
			byte[] array = ((codedCharSetId == null || codedCharSetId.Value <= 0) ? Encoding.UTF8.GetBytes(value) : HisEncoding.BytesFromString(value, codedCharSetId.Value));
			if (fixedLength != null && fixedLength.Value != array.Length)
			{
				byte[] array2 = new byte[fixedLength.Value];
				int num = ((array.Length > fixedLength.Value) ? fixedLength.Value : array.Length);
				Array.Copy(array, array2, num);
				return array2;
			}
			return array;
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000E6214 File Offset: 0x000E4414
		public static Value ValueFromBytes(byte[] value, Value encodingValue, TypeValue returnType, int ccsid = 0)
		{
			if (value == null)
			{
				return Value.Null;
			}
			if (encodingValue.IsNull || encodingValue.IsType)
			{
				if (object.Equals(returnType, TypeValue.Binary))
				{
					return BinaryValue.New(value);
				}
				if (object.Equals(returnType, TypeValue.Text))
				{
					return TextValue.New(HisEncoding.StringFromBytes(value, ccsid));
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			else
			{
				if (encodingValue.Equals(Library.BinaryEncoding.Base64) || encodingValue.Equals(Library.BinaryEncoding.Hex))
				{
					return Library.Binary.ToText.Invoke(BinaryValue.New(value), encodingValue);
				}
				string text;
				try
				{
					text = TextEncoding.GetTextDecoder(encodingValue).Decode(value, 0, value.Length);
				}
				catch (ArgumentException)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.Text_InvalidUnicodeCodePoints, Value.Null, null);
				}
				return TextValue.NewOrNull(text);
			}
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x000E62E0 File Offset: 0x000E44E0
		public static bool TryGetIdentifier(Value optionValue, int? maxLength, out object objectValue)
		{
			if (optionValue.IsBinary)
			{
				byte[] asBytes = optionValue.AsBinary.AsBytes;
				if (maxLength != null && asBytes.Length == maxLength.Value)
				{
					objectValue = asBytes;
					return true;
				}
			}
			else if (optionValue.IsText)
			{
				objectValue = Utilities.BytesFromString(optionValue.AsString, maxLength, null);
				return true;
			}
			objectValue = null;
			return false;
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x000E6340 File Offset: 0x000E4540
		public static bool TryGetBinaryOrText(Value optionValue, int? maxLength, out object objectValue)
		{
			if (maxLength != null)
			{
				return Utilities.TryGetIdentifier(optionValue, maxLength, out objectValue);
			}
			if (optionValue.IsBinary)
			{
				objectValue = optionValue.AsBinary.AsBytes;
				return true;
			}
			if (optionValue.IsText)
			{
				objectValue = Utilities.BytesFromString(optionValue.AsString, null, null);
				return true;
			}
			objectValue = null;
			return false;
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x000E63A4 File Offset: 0x000E45A4
		public static bool TryGetString(Value optionValue, int? maxLength, out object objectValue)
		{
			if (optionValue.IsText)
			{
				string asString = optionValue.AsString;
				if (maxLength == null || asString.Length <= maxLength.Value)
				{
					objectValue = asString;
					return true;
				}
			}
			objectValue = null;
			return false;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x000E63E4 File Offset: 0x000E45E4
		public static bool TryGetInt(Value optionValue, int? maxLength, out object objectValue)
		{
			if (optionValue.IsNumber)
			{
				int asInteger = optionValue.AsInteger32;
				if (maxLength == null || asInteger <= maxLength.Value)
				{
					objectValue = asInteger;
					return true;
				}
			}
			objectValue = null;
			return false;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000E6424 File Offset: 0x000E4624
		public static bool TryGetMessageType(Value optionValue, int? maxLength, out object messageType)
		{
			if (optionValue.IsText)
			{
				string asString = optionValue.AsString;
				if (asString == "Datagram")
				{
					messageType = MessageType.Datagram;
					return true;
				}
				if (asString == "Reply")
				{
					messageType = MessageType.Reply;
					return true;
				}
				if (asString == "Request")
				{
					messageType = MessageType.Request;
					return true;
				}
			}
			messageType = MessageType.None;
			return false;
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x000E6490 File Offset: 0x000E4690
		public static bool TryGetCcsid(Value optionValue, int? maxLength, out object objectValue)
		{
			int num;
			if (optionValue.IsNumber && optionValue.AsNumber.TryGetInt32(out num) && num >= 37 && num <= 65520)
			{
				objectValue = num;
				return true;
			}
			objectValue = null;
			return false;
		}

		// Token: 0x04002474 RID: 9332
		private static bool? isNet46Installed;
	}
}
