using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FE7 RID: 4071
	internal sealed class ActiveDirectoryValueBuilder
	{
		// Token: 0x06006ADF RID: 27359 RVA: 0x0017062C File Offset: 0x0016E82C
		public ActiveDirectoryValueBuilder(ActiveDirectoryServiceAccessor service, ActiveDirectoryTypeCatalog typeCatalog)
		{
			this.service = service;
			this.typeCatalog = typeCatalog;
			this.AddSyntax("2.5.5.1", new Func<string, TypeValue>(this.ToDistinguishedNamedType), new Func<string, object, IValueReference>(this.ToDistinguishedNameValue));
			this.AddSyntax("2.5.5.2", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.3", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.4", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.5", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.6", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.7", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.8", TypeValue.Logical.Nullable, (object o) => LogicalValue.New((bool)o));
			this.AddSyntax("2.5.5.9", TypeValue.Number.Nullable, (object o) => NumberValue.New((int)o));
			this.AddSyntax("2.5.5.10", TypeValue.Binary.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToBinaryValue));
			this.AddSyntax("2.5.5.11", TypeValue.DateTimeZone.Nullable, new Func<object, Value>(this.ToDateTimeValue));
			this.AddSyntax("2.5.5.12", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.13", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.14", TypeValue.Text.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToTextValue));
			this.AddSyntax("2.5.5.15", TypeValue.Binary.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToBinaryValue));
			this.AddSyntax("2.5.5.16", ActiveDirectoryValueBuilder.LargeNumberType, (object o) => NumberValue.New(new decimal((long)o)));
			this.AddSyntax("2.5.5.17", TypeValue.Binary.Nullable, new Func<object, Value>(ActiveDirectoryValueBuilder.ToBinaryValue));
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x001708CC File Offset: 0x0016EACC
		public IValueReference CreateValue(string objectCategory, string attributeName, ActiveDirectoryServiceSearchResult searchResult, string searchHost = null, string distinguishedName = null)
		{
			object[] array;
			if (!searchResult.TryGetAttribute(attributeName, out array))
			{
				return Value.Null;
			}
			if ((array == null || array.Length == 0) && searchHost != null && distinguishedName != null && string.Equals(objectCategory, "group", StringComparison.OrdinalIgnoreCase) && string.Equals(attributeName, "member", StringComparison.OrdinalIgnoreCase))
			{
				return new ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue(this, searchResult, searchHost, distinguishedName);
			}
			return this.CreateValue(objectCategory, attributeName, array);
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0017092C File Offset: 0x0016EB2C
		public TypeValue CreateAttributeTypeValue(string attributeName)
		{
			ActiveDirectoryAttributeSchema attribute = this.typeCatalog.GetAttribute(attributeName);
			TypeValue typeValue = this.GetSyntax(attribute).GetTypeValue(attributeName);
			if (!attribute.IsSingleValued)
			{
				return ListTypeValue.New(typeValue).Nullable;
			}
			return typeValue;
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x00170970 File Offset: 0x0016EB70
		public RecordTypeValue CreateRecordTypeValue(string objectCategoryName, Keys attributeNames)
		{
			Value[] array = new Value[attributeNames.Length];
			for (int i = 0; i < array.Length; i++)
			{
				TypeValue typeValue = this.CreateAttributeTypeValue(attributeNames[i]);
				array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					LogicalValue.False
				});
			}
			return RecordTypeValue.New(RecordValue.New(attributeNames, array));
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x001709D0 File Offset: 0x0016EBD0
		private IValueReference CreateValue(string objectCategoryName, string attributeName, object[] value)
		{
			ActiveDirectoryClassSchema objectClass = this.typeCatalog.GetObjectClass(objectCategoryName);
			return this.CreateValue(objectClass, attributeName, value);
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x001709F4 File Offset: 0x0016EBF4
		private IValueReference CreateValue(ActiveDirectoryClassSchema objectCategory, string attributeName, object[] value)
		{
			ActiveDirectoryAttributeSchema attribute = this.typeCatalog.GetAttribute(attributeName);
			ActiveDirectoryValueBuilder.AttributeSyntax attributeType = this.GetSyntax(attribute);
			if (attribute.IsSingleValued)
			{
				return attributeType.Marshal(attributeName, value[0]);
			}
			return ListValue.New(value.Length, (int i) => attributeType.Marshal(attributeName, value[i]).Value);
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x00170A74 File Offset: 0x0016EC74
		private ActiveDirectoryValueBuilder.AttributeSyntax GetSyntax(ActiveDirectoryAttributeSchema attributeClass)
		{
			ActiveDirectoryValueBuilder.AttributeSyntax attributeSyntax;
			if (!this.syntaxTypes.TryGetValue(attributeClass.Syntax, out attributeSyntax))
			{
				throw ActiveDirectoryExceptions.NewUnsupportedAttributeSyntaxException(this.service.Host, attributeClass.Syntax, attributeClass.LdapDisplayName, this.service.Resource);
			}
			return attributeSyntax;
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x00170AC0 File Offset: 0x0016ECC0
		private DateTimeZoneValue ToDateTimeValue(object o)
		{
			return DateTimeZoneValue.New(((DateTime)o).Ticks, 0);
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x00170AE1 File Offset: 0x0016ECE1
		private TypeValue ToDistinguishedNamedType(string attributeName)
		{
			if (attributeName.Equals("distinguishedName"))
			{
				return TypeValue.Text.Nullable;
			}
			return PreviewServices.ConvertToDelayedValue(TypeValue.Record.Nullable, "Record");
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x00170B10 File Offset: 0x0016ED10
		private IValueReference ToDistinguishedNameValue(string attributeName, object o)
		{
			string distinguishedName = (string)o;
			if (attributeName.Equals("distinguishedName"))
			{
				return TextValue.New(distinguishedName);
			}
			return new DelayedValue(delegate
			{
				ActiveDirectoryServiceSearchResult result;
				if (!this.service.TryGetObject(distinguishedName, new string[0], out result))
				{
					return Value.Null;
				}
				object[] attribute = result.GetAttribute("objectClass");
				string objectClass = (string)attribute[attribute.Length - 1];
				Keys attributeNames;
				RecordTypeValue recordTypeValue;
				if (!this.objectClassTypes.TryGetValue(objectClass, out recordTypeValue))
				{
					ActiveDirectoryClassSchema objectClass2 = this.typeCatalog.GetObjectClass(objectClass);
					attributeNames = Keys.New(this.typeCatalog.GetAllObjectClassAttributeNames(objectClass2.Name));
					recordTypeValue = this.CreateRecordTypeValue(objectClass2.Name, attributeNames);
					this.objectClassTypes.Add(objectClass, recordTypeValue);
				}
				else
				{
					attributeNames = recordTypeValue.Fields.Keys;
				}
				return RecordValue.New(recordTypeValue, (int i) => this.CreateValue(objectClass, attributeNames[i], result, null, null).Value);
			});
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x00170B60 File Offset: 0x0016ED60
		private void AddSyntax(string syntax, TypeValue typeValue, Func<object, Value> marshaller)
		{
			this.AddSyntax(syntax, (string s) => typeValue, (string s, object o) => marshaller(o));
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x00170BA0 File Offset: 0x0016EDA0
		private void AddSyntax(string syntax, Func<string, TypeValue> typeValue, Func<string, object, IValueReference> marshaller)
		{
			this.syntaxTypes.Add(syntax, new ActiveDirectoryValueBuilder.AttributeSyntax
			{
				GetTypeValue = typeValue,
				Marshal = marshaller
			});
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x00170BC1 File Offset: 0x0016EDC1
		private static Value ToTextValue(object o)
		{
			return TextValue.New((string)o);
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x00170BCE File Offset: 0x0016EDCE
		private static Value ToBinaryValue(object o)
		{
			return BinaryValue.New((byte[])o);
		}

		// Token: 0x04003B69 RID: 15209
		private static readonly TypeValue LargeNumberType = TypeValue.Number.Nullable.NewMeta(TypeServices.WindowsNTFileTimeFormatMetadataRecord).AsType;

		// Token: 0x04003B6A RID: 15210
		private readonly ActiveDirectoryServiceAccessor service;

		// Token: 0x04003B6B RID: 15211
		private readonly ActiveDirectoryTypeCatalog typeCatalog;

		// Token: 0x04003B6C RID: 15212
		private readonly Dictionary<string, RecordTypeValue> objectClassTypes = new Dictionary<string, RecordTypeValue>();

		// Token: 0x04003B6D RID: 15213
		private readonly Dictionary<string, ActiveDirectoryValueBuilder.AttributeSyntax> syntaxTypes = new Dictionary<string, ActiveDirectoryValueBuilder.AttributeSyntax>();

		// Token: 0x02000FE8 RID: 4072
		private class AttributeSyntax
		{
			// Token: 0x17001E9C RID: 7836
			// (get) Token: 0x06006AEE RID: 27374 RVA: 0x00170BFB File Offset: 0x0016EDFB
			// (set) Token: 0x06006AEF RID: 27375 RVA: 0x00170C03 File Offset: 0x0016EE03
			public Func<string, TypeValue> GetTypeValue { get; set; }

			// Token: 0x17001E9D RID: 7837
			// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x00170C0C File Offset: 0x0016EE0C
			// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x00170C14 File Offset: 0x0016EE14
			public Func<string, object, IValueReference> Marshal { get; set; }
		}

		// Token: 0x02000FE9 RID: 4073
		private class ActiveDirectoryGroupMembersListValue : StreamedListValue
		{
			// Token: 0x06006AF3 RID: 27379 RVA: 0x00170C20 File Offset: 0x0016EE20
			public ActiveDirectoryGroupMembersListValue(ActiveDirectoryValueBuilder valueBuilder, ActiveDirectoryServiceSearchResult searchResult, string searchHost, string distinguishedName)
			{
				this.members = new List<object>();
				this.searchHost = searchHost;
				this.distinguishedName = distinguishedName;
				this.valueBuilder = valueBuilder;
				this.attributeType = this.valueBuilder.GetSyntax(this.valueBuilder.typeCatalog.GetAttribute("member"));
				string text;
				object[] array;
				if (ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue.GetMemberRangeValues(searchResult, out text, out array))
				{
					this.members.AddRange(array);
					if (ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue.IsEndOfRange(text))
					{
						this.reachedEnd = true;
					}
				}
			}

			// Token: 0x06006AF4 RID: 27380 RVA: 0x00170CA1 File Offset: 0x0016EEA1
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue.ActiveDirectoryGroupMembersEnumerator(this);
			}

			// Token: 0x06006AF5 RID: 27381 RVA: 0x00170CA9 File Offset: 0x0016EEA9
			private ActiveDirectoryServiceSearchResult Search(string[] columns)
			{
				return this.valueBuilder.service.GetObject(this.searchHost, this.distinguishedName, columns);
			}

			// Token: 0x06006AF6 RID: 27382 RVA: 0x00170CC8 File Offset: 0x0016EEC8
			private static bool GetMemberRangeValues(ActiveDirectoryServiceSearchResult searchResult, out string memberAttributeName, out object[] memberValues)
			{
				memberAttributeName = searchResult.AttributeNames.FirstOrDefault((string attribute) => attribute != null && attribute.StartsWith("member;range=", StringComparison.OrdinalIgnoreCase));
				if (memberAttributeName != null)
				{
					return searchResult.TryGetAttribute(memberAttributeName, out memberValues);
				}
				memberValues = null;
				return false;
			}

			// Token: 0x06006AF7 RID: 27383 RVA: 0x00170D08 File Offset: 0x0016EF08
			private static bool IsEndOfRange(string memberAttributeName)
			{
				return memberAttributeName.EndsWith("-*", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04003B70 RID: 15216
			private const string AttributeName = "member";

			// Token: 0x04003B71 RID: 15217
			private const string RequestPrefix = "member;range=";

			// Token: 0x04003B72 RID: 15218
			private const string RequestTemplate = "member;range={0}-{1}";

			// Token: 0x04003B73 RID: 15219
			private readonly List<object> members;

			// Token: 0x04003B74 RID: 15220
			private readonly ActiveDirectoryValueBuilder valueBuilder;

			// Token: 0x04003B75 RID: 15221
			private readonly ActiveDirectoryValueBuilder.AttributeSyntax attributeType;

			// Token: 0x04003B76 RID: 15222
			private readonly string searchHost;

			// Token: 0x04003B77 RID: 15223
			private readonly string distinguishedName;

			// Token: 0x04003B78 RID: 15224
			private bool reachedEnd;

			// Token: 0x02000FEA RID: 4074
			private class ActiveDirectoryGroupMembersEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06006AF8 RID: 27384 RVA: 0x00170D16 File Offset: 0x0016EF16
				public ActiveDirectoryGroupMembersEnumerator(ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue groupMembers)
				{
					this.groupMembers = groupMembers;
					this.currentIndex = -1;
				}

				// Token: 0x06006AF9 RID: 27385 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x17001E9E RID: 7838
				// (get) Token: 0x06006AFA RID: 27386 RVA: 0x00170D2C File Offset: 0x0016EF2C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06006AFB RID: 27387 RVA: 0x00170D34 File Offset: 0x0016EF34
				public void Reset()
				{
					this.currentIndex = -1;
				}

				// Token: 0x17001E9F RID: 7839
				// (get) Token: 0x06006AFC RID: 27388 RVA: 0x00170D40 File Offset: 0x0016EF40
				public IValueReference Current
				{
					get
					{
						if (this.currentIndex < 0)
						{
							throw new InvalidOperationException();
						}
						object obj = this.groupMembers.members[this.currentIndex];
						if (obj == null)
						{
							return Value.Null;
						}
						IValueReference valueReference = obj as IValueReference;
						if (valueReference == null)
						{
							valueReference = this.groupMembers.attributeType.Marshal("member", obj);
							this.groupMembers.members[this.currentIndex] = valueReference;
						}
						return valueReference;
					}
				}

				// Token: 0x06006AFD RID: 27389 RVA: 0x00170DBC File Offset: 0x0016EFBC
				public bool MoveNext()
				{
					if (this.groupMembers.members.Count == 0)
					{
						return false;
					}
					if (this.currentIndex < this.groupMembers.members.Count - 1)
					{
						this.currentIndex++;
						return true;
					}
					if (this.groupMembers.reachedEnd)
					{
						return false;
					}
					string[] array = new string[] { string.Format(CultureInfo.InvariantCulture, "member;range={0}-{1}", this.groupMembers.members.Count, "*") };
					string text;
					object[] array2;
					if (ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue.GetMemberRangeValues(this.groupMembers.Search(array), out text, out array2))
					{
						if (ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue.IsEndOfRange(text))
						{
							this.groupMembers.reachedEnd = true;
						}
						if (array2.Length != 0)
						{
							this.groupMembers.members.AddRange(array2);
							this.currentIndex++;
							return true;
						}
						this.groupMembers.reachedEnd = true;
					}
					return false;
				}

				// Token: 0x04003B79 RID: 15225
				private readonly ActiveDirectoryValueBuilder.ActiveDirectoryGroupMembersListValue groupMembers;

				// Token: 0x04003B7A RID: 15226
				private int currentIndex;
			}
		}
	}
}
