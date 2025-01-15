using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000293 RID: 659
	internal static class XmlTableValue
	{
		// Token: 0x06001AAF RID: 6831 RVA: 0x000360C8 File Offset: 0x000342C8
		public static TableTypeValue TypeForTable(Keys keys, Value[] fieldDescriptors)
		{
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, fieldDescriptors)));
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000360DB File Offset: 0x000342DB
		public static RecordValue FieldDescriptor(TypeValue fieldType)
		{
			return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				fieldType,
				LogicalValue.False
			});
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000360FC File Offset: 0x000342FC
		private static TableTypeValue CreateType(Keys columnNames, RecordValue record)
		{
			Value[] array = new Value[columnNames.Length];
			for (int i = 0; i < columnNames.Length; i++)
			{
				array[i] = XmlTableValue.FieldDescriptor(record[i].Type);
			}
			return XmlTableValue.TypeForTable(columnNames, array);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00036144 File Offset: 0x00034344
		public static TableValue FromReader(XmlReader reader, XmlTableOptions options)
		{
			TableValue tableValue = XmlTableValue.FromRow(new XmlTableValue.XmlRowReader(reader).Read(options, reader.NamespaceURI));
			if (options.ConsiderNavShape)
			{
				tableValue = XmlTableValue.AttemptNavPivot(tableValue);
			}
			return tableValue;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00036180 File Offset: 0x00034380
		public static TableValue FromRow(RecordValue record)
		{
			TableTypeValue tableTypeValue = XmlTableValue.CreateType(record.Keys, record);
			return ListValue.New(new Value[] { record }).ToTable(tableTypeValue);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000361AF File Offset: 0x000343AF
		private static RecordValue MakeEmptyTextElement()
		{
			XmlColumnMap xmlColumnMap = new XmlColumnMap();
			xmlColumnMap.AddText(string.Empty);
			return xmlColumnMap.ToRow(new XmlTableOptions(Value.Null));
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000361D0 File Offset: 0x000343D0
		private static TableValue AttemptNavPivot(TableValue table)
		{
			RecordValue recordValue;
			if (XmlTableValue.TryGetNavPivotCandidate(table, out recordValue))
			{
				Value[] array = new Value[recordValue.Keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = RecordValue.New(XmlTableValue.navColumnNames, new Value[]
					{
						TextValue.New(recordValue.Keys[i]),
						XmlTableValue.AttemptNavPivot(recordValue[i].AsTable)
					});
				}
				return ListValue.New(array).ToTable(XmlTableValue.navTableType);
			}
			return table;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00036254 File Offset: 0x00034454
		private static bool TryGetNavPivotCandidate(TableValue table, out RecordValue row)
		{
			row = null;
			foreach (IValueReference valueReference in table)
			{
				if (row != null)
				{
					return false;
				}
				row = valueReference.Value.AsRecord;
				for (int i = 0; i < row.Keys.Length; i++)
				{
					if (!row[i].IsTable)
					{
						return false;
					}
				}
			}
			return row != null;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000362E0 File Offset: 0x000344E0
		public static void Serialize(TableValue table, IStorage storage, IEngineHost host)
		{
			new XmlTableValue.Serializer(storage, host).Serialize(table);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000362EF File Offset: 0x000344EF
		public static TableValue Deserialize(IStorage storage, IEngineHost host)
		{
			return XmlTableValue.Deserializer.Deserialize(storage, 0).AsTable;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00036300 File Offset: 0x00034500
		// Note: this type is marked as 'beforefieldinit'.
		static XmlTableValue()
		{
			Keys keys = Keys.New("NavigationTable.NameColumn", "NavigationTable.DataColumn");
			Value[] array = new TextValue[]
			{
				TextValue.New("Name"),
				TextValue.New("Table")
			};
			XmlTableValue.navMeta = RecordValue.New(keys, array);
			XmlTableValue.emptyTextElement = XmlTableValue.MakeEmptyTextElement();
			Keys keys2 = XmlTableValue.navColumnNames;
			array = new RecordValue[]
			{
				XmlTableValue.FieldDescriptor(TypeValue.Text),
				XmlTableValue.FieldDescriptor(TypeValue.Table)
			};
			XmlTableValue.navTableType = XmlTableValue.TypeForTable(keys2, array).NewMeta(XmlTableValue.navMeta).AsType.AsTableType;
		}

		// Token: 0x040007FD RID: 2045
		private static readonly Keys navColumnNames = Keys.New("Name", "Table");

		// Token: 0x040007FE RID: 2046
		private static readonly RecordValue navMeta;

		// Token: 0x040007FF RID: 2047
		private static readonly RecordValue emptyTextElement;

		// Token: 0x04000800 RID: 2048
		public static readonly TableTypeValue navTableType;

		// Token: 0x04000801 RID: 2049
		private const string xmlNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x02000294 RID: 660
		private struct XmlRowReader
		{
			// Token: 0x06001ABA RID: 6842 RVA: 0x000363AB File Offset: 0x000345AB
			public XmlRowReader(XmlReader reader)
			{
				this.reader = reader;
				this.elements = new List<XmlTableValue.XmlRowReader.Element>();
				this.attributes = new List<XmlTableValue.XmlRowReader.Attribute>();
			}

			// Token: 0x06001ABB RID: 6843 RVA: 0x000363CC File Offset: 0x000345CC
			public RecordValue Read(XmlTableOptions options, string primaryNs)
			{
				bool flag;
				return this.Read(options, primaryNs, out flag);
			}

			// Token: 0x06001ABC RID: 6844 RVA: 0x000363E4 File Offset: 0x000345E4
			private RecordValue Read(XmlTableOptions options, string primaryNs, out bool wasEmpty)
			{
				wasEmpty = false;
				int count = this.elements.Count;
				int count2 = this.attributes.Count;
				StringBuilder stringBuilder = null;
				string text = null;
				HashSet<XmlTableValue.XmlRowReader.Element> hashSet = null;
				if (this.reader.AttributeCount > 0)
				{
					for (int i = 0; i < this.reader.AttributeCount; i++)
					{
						this.reader.MoveToAttribute(i);
						this.attributes.Add(new XmlTableValue.XmlRowReader.Attribute(this.reader.LocalName, this.reader.NamespaceURI, this.reader.Value));
					}
					if (!this.reader.MoveToElement())
					{
						throw new InvalidOperationException();
					}
				}
				if (this.reader.IsEmptyElement)
				{
					wasEmpty = this.reader.AttributeCount == 0;
					this.reader.Read();
				}
				else
				{
					this.reader.ReadStartElement();
					while (this.reader.NodeType != XmlNodeType.EndElement)
					{
						if (this.reader.NodeType == XmlNodeType.Element)
						{
							string localName = this.reader.LocalName;
							string namespaceURI = this.reader.NamespaceURI;
							bool flag;
							RecordValue recordValue = this.Read(options, namespaceURI, out flag);
							XmlTableValue.XmlRowReader.Element element = new XmlTableValue.XmlRowReader.Element(localName, namespaceURI, recordValue);
							if (flag)
							{
								hashSet = hashSet ?? new HashSet<XmlTableValue.XmlRowReader.Element>();
								hashSet.Add(element);
							}
							this.elements.Add(element);
						}
						else
						{
							if (count == this.elements.Count)
							{
								if (stringBuilder != null)
								{
									stringBuilder.Append(this.reader.Value);
								}
								else if (text != null)
								{
									stringBuilder = new StringBuilder(text);
									stringBuilder.Append(this.reader.Value);
									text = null;
								}
								else
								{
									text = this.reader.Value;
								}
							}
							if (!this.reader.Read())
							{
								throw new InvalidOperationException();
							}
						}
					}
					this.reader.ReadEndElement();
				}
				XmlColumnMap xmlColumnMap = new XmlColumnMap();
				if (count != this.elements.Count)
				{
					for (int j = count; j < this.elements.Count; j++)
					{
						XmlTableValue.XmlRowReader.Element element2 = this.elements[j];
						RecordValue recordValue2 = element2.Value;
						if (hashSet != null && hashSet.Contains(element2) && count2 == this.attributes.Count)
						{
							recordValue2 = XmlTableValue.emptyTextElement;
						}
						xmlColumnMap.AddElement(element2.LocalName, element2.NamespaceURI, recordValue2, primaryNs);
					}
					this.elements.RemoveRange(count, this.elements.Count - count);
				}
				else
				{
					if (stringBuilder != null)
					{
						text = stringBuilder.ToString();
					}
					if (text != null)
					{
						xmlColumnMap.AddText(text);
					}
				}
				if (count2 != this.attributes.Count)
				{
					for (int k = count2; k < this.attributes.Count; k++)
					{
						XmlTableValue.XmlRowReader.Attribute attribute = this.attributes[k];
						if (attribute.NamespaceURI != "http://www.w3.org/2000/xmlns/")
						{
							xmlColumnMap.AddAttribute(attribute.LocalName, attribute.NamespaceURI, attribute.Value);
						}
					}
					this.attributes.RemoveRange(count2, this.attributes.Count - count2);
				}
				return xmlColumnMap.ToRow(options);
			}

			// Token: 0x04000802 RID: 2050
			private XmlReader reader;

			// Token: 0x04000803 RID: 2051
			private List<XmlTableValue.XmlRowReader.Element> elements;

			// Token: 0x04000804 RID: 2052
			private List<XmlTableValue.XmlRowReader.Attribute> attributes;

			// Token: 0x02000295 RID: 661
			private struct Element
			{
				// Token: 0x06001ABD RID: 6845 RVA: 0x000366F4 File Offset: 0x000348F4
				public Element(string localName, string ns, RecordValue value)
				{
					this.localName = localName;
					this.ns = ns;
					this.value = value;
				}

				// Token: 0x17000CF0 RID: 3312
				// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0003670B File Offset: 0x0003490B
				public string LocalName
				{
					get
					{
						return this.localName;
					}
				}

				// Token: 0x17000CF1 RID: 3313
				// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00036713 File Offset: 0x00034913
				public string NamespaceURI
				{
					get
					{
						return this.ns;
					}
				}

				// Token: 0x17000CF2 RID: 3314
				// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0003671B File Offset: 0x0003491B
				public RecordValue Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x04000805 RID: 2053
				private string localName;

				// Token: 0x04000806 RID: 2054
				private string ns;

				// Token: 0x04000807 RID: 2055
				private RecordValue value;
			}

			// Token: 0x02000296 RID: 662
			private struct Attribute
			{
				// Token: 0x06001AC1 RID: 6849 RVA: 0x00036723 File Offset: 0x00034923
				public Attribute(string localName, string ns, string value)
				{
					this.localName = localName;
					this.ns = ns;
					this.value = value;
				}

				// Token: 0x17000CF3 RID: 3315
				// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0003673A File Offset: 0x0003493A
				public string LocalName
				{
					get
					{
						return this.localName;
					}
				}

				// Token: 0x17000CF4 RID: 3316
				// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00036742 File Offset: 0x00034942
				public string NamespaceURI
				{
					get
					{
						return this.ns;
					}
				}

				// Token: 0x17000CF5 RID: 3317
				// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0003674A File Offset: 0x0003494A
				public string Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x04000808 RID: 2056
				private string localName;

				// Token: 0x04000809 RID: 2057
				private string ns;

				// Token: 0x0400080A RID: 2058
				private string value;
			}
		}

		// Token: 0x02000297 RID: 663
		public class Serializer
		{
			// Token: 0x06001AC5 RID: 6853 RVA: 0x00036752 File Offset: 0x00034952
			public Serializer(IStorage storage, IEngineHost host)
			{
				this.storage = storage;
				this.tempDirectoryService = host.QueryService<ITempDirectoryService>();
				this.queue = new Queue<Tuple<int, Value>>();
			}

			// Token: 0x06001AC6 RID: 6854 RVA: 0x00036778 File Offset: 0x00034978
			public void Serialize(Value value)
			{
				Queue<Tuple<int, Value>> queue = this.queue;
				int num = this.id;
				this.id = num + 1;
				queue.Enqueue(new Tuple<int, Value>(num, value));
				while (this.queue.Count > 0)
				{
					Tuple<int, Value> tuple = this.queue.Dequeue();
					Stream stream = this.storage.CreateStream();
					if (stream.CanSeek)
					{
						this.writer = new StreamValueWriter(stream);
						this.WriteValueInline(tuple.Item2);
					}
					else
					{
						using (Stream stream2 = this.tempDirectoryService.CreateFile())
						{
							this.writer = new StreamValueWriter(stream2);
							this.WriteValueInline(tuple.Item2);
							stream2.Position = 0L;
							stream2.CopyTo(stream);
						}
					}
					this.storage.CommitStream(tuple.Item1, stream).Close();
				}
			}

			// Token: 0x06001AC7 RID: 6855 RVA: 0x00036860 File Offset: 0x00034A60
			private void WriteValue(Value value)
			{
				if (value.IsTable)
				{
					this.WriteReference(value.AsTable);
					return;
				}
				this.WriteValueInline(value);
			}

			// Token: 0x06001AC8 RID: 6856 RVA: 0x00036880 File Offset: 0x00034A80
			private void WriteValueInline(Value value)
			{
				ValueFlags valueFlags = ValueHelper.Flags(value);
				this.writer.WriteStartValue(value.Kind, valueFlags);
				if ((valueFlags & ValueFlags.HasMeta) == ValueFlags.HasMeta)
				{
					this.WriteValue(value.MetaValue);
				}
				if ((valueFlags & ValueFlags.HasType) == ValueFlags.HasType)
				{
					this.WriteValue(value.Type);
				}
				ValueKind kind = value.Kind;
				if (kind != ValueKind.Null)
				{
					switch (kind)
					{
					case ValueKind.Logical:
						this.writer.WriteLogical(value.AsBoolean);
						goto IL_00D8;
					case ValueKind.Text:
						this.writer.WriteText(value.AsString);
						goto IL_00D8;
					case ValueKind.Record:
						this.WriteRecord(value.AsRecord);
						goto IL_00D8;
					case ValueKind.Table:
						this.WriteTable(value.AsTable);
						goto IL_00D8;
					case ValueKind.Type:
						this.WriteType(value.AsType);
						goto IL_00D8;
					}
					throw new InvalidOperationException();
				}
				this.writer.WriteNull();
				IL_00D8:
				this.writer.WriteEndValue();
			}

			// Token: 0x06001AC9 RID: 6857 RVA: 0x00036970 File Offset: 0x00034B70
			private void WriteRecord(RecordValue record)
			{
				Keys keys = record.Keys;
				this.writer.WriteStartRecord(keys);
				for (int i = 0; i < keys.Length; i++)
				{
					this.WriteValue(record[i]);
				}
				this.writer.WriteEndRecord();
			}

			// Token: 0x06001ACA RID: 6858 RVA: 0x000369BC File Offset: 0x00034BBC
			private void WriteTable(TableValue table)
			{
				Keys columns = table.Columns;
				this.writer.WriteStartTable(columns);
				foreach (IValueReference valueReference in table)
				{
					this.WriteRow(valueReference.Value.AsRecord, columns);
				}
				this.writer.WriteEndTable();
			}

			// Token: 0x06001ACB RID: 6859 RVA: 0x00036A30 File Offset: 0x00034C30
			private void WriteReference(Value value)
			{
				int num = this.id;
				this.id = num + 1;
				int num2 = num;
				this.writer.WriteStartValue(ValueKind.Reference, ValueFlags.None);
				this.writer.WriteNumber(num2);
				this.writer.WriteEndValue();
				this.queue.Enqueue(new Tuple<int, Value>(num2, value));
			}

			// Token: 0x06001ACC RID: 6860 RVA: 0x00036A88 File Offset: 0x00034C88
			private void WriteRow(RecordValue row, Keys columns)
			{
				this.writer.WriteStartRow();
				for (int i = 0; i < columns.Length; i++)
				{
					this.WriteValue(row[i]);
				}
				this.writer.WriteEndRow();
			}

			// Token: 0x06001ACD RID: 6861 RVA: 0x00036ACC File Offset: 0x00034CCC
			private void WriteType(TypeValue type)
			{
				this.writer.WriteStartType(type.TypeKind, type.IsNullable);
				ValueKind typeKind = type.TypeKind;
				if (typeKind != ValueKind.Any && typeKind != ValueKind.Text && typeKind == ValueKind.Table)
				{
					this.WriteTableType(type.AsTableType);
				}
				this.writer.WriteEndType();
			}

			// Token: 0x06001ACE RID: 6862 RVA: 0x00036B1C File Offset: 0x00034D1C
			private void WriteRecordType(RecordTypeValue recordType)
			{
				Keys keys = recordType.Fields.Keys;
				this.writer.WriteStartRecordType(keys, recordType.Open);
				for (int i = 0; i < keys.Length; i++)
				{
					this.WriteValue(recordType.Fields[i]["Type"].AsType);
				}
				this.writer.WriteEndRecordType();
			}

			// Token: 0x06001ACF RID: 6863 RVA: 0x00036B84 File Offset: 0x00034D84
			private void WriteTableType(TableTypeValue tableType)
			{
				this.writer.WriteTableType(XmlTableValue.Serializer.emptyTableKeys);
				this.WriteRecordType(tableType.ItemType);
			}

			// Token: 0x0400080B RID: 2059
			private static TableKey[] emptyTableKeys = new TableKey[0];

			// Token: 0x0400080C RID: 2060
			private IValueWriter writer;

			// Token: 0x0400080D RID: 2061
			private IStorage storage;

			// Token: 0x0400080E RID: 2062
			private int id;

			// Token: 0x0400080F RID: 2063
			private Queue<Tuple<int, Value>> queue;

			// Token: 0x04000810 RID: 2064
			private ITempDirectoryService tempDirectoryService;
		}

		// Token: 0x02000298 RID: 664
		public class Deserializer : IDisposable
		{
			// Token: 0x06001AD1 RID: 6865 RVA: 0x00036BB0 File Offset: 0x00034DB0
			public static Value Deserialize(IStorage storage, int id)
			{
				Value value;
				using (XmlTableValue.Deserializer deserializer = new XmlTableValue.Deserializer(storage, id))
				{
					value = deserializer.ReadValue().Value;
				}
				return value;
			}

			// Token: 0x06001AD2 RID: 6866 RVA: 0x00036BF0 File Offset: 0x00034DF0
			public Deserializer(IStorage storage, int id)
			{
				this.storage = storage;
				this.id = id;
				Stream stream = storage.OpenStream(id);
				if (!stream.CanSeek)
				{
					stream = new ForwardSeekableReadOnlyStream(storage.OpenStream(id));
				}
				this.reader = new StreamValueReader(stream);
			}

			// Token: 0x06001AD3 RID: 6867 RVA: 0x00036C3A File Offset: 0x00034E3A
			void IDisposable.Dispose()
			{
				this.reader.Close();
			}

			// Token: 0x17000CF6 RID: 3318
			// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00036C47 File Offset: 0x00034E47
			// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00036C59 File Offset: 0x00034E59
			public long Position
			{
				get
				{
					return this.reader.BaseStream.Position;
				}
				set
				{
					this.reader.BaseStream.Position = value;
				}
			}

			// Token: 0x06001AD6 RID: 6870 RVA: 0x00036C6C File Offset: 0x00034E6C
			private IValueReference ReadValue()
			{
				ValueKind valueKind;
				ValueFlags valueFlags;
				this.reader.ReadStartValue(out valueKind, out valueFlags);
				RecordValue recordValue = (((valueFlags & ValueFlags.HasMeta) == ValueFlags.HasMeta) ? this.ReadValue().Value.AsRecord : null);
				TypeValue typeValue = (((valueFlags & ValueFlags.HasType) == ValueFlags.HasType) ? this.ReadValue().Value.AsType : null);
				IValueReference valueReference;
				if (valueKind != ValueKind.Reference)
				{
					if (valueKind != ValueKind.Null)
					{
						switch (valueKind)
						{
						case ValueKind.Logical:
						case ValueKind.Text:
							goto IL_0079;
						case ValueKind.Record:
							valueReference = this.ReadRecord();
							goto IL_00B3;
						case ValueKind.Table:
							valueReference = this.ReadTable(typeValue);
							goto IL_00B3;
						case ValueKind.Type:
							valueReference = this.ReadType();
							goto IL_00B3;
						}
						throw new InvalidOperationException();
					}
					IL_0079:
					valueReference = this.ReadPrimitiveValue(valueKind);
				}
				else
				{
					valueReference = this.ReadReference();
				}
				IL_00B3:
				if (recordValue != null)
				{
					valueReference = valueReference.Value.NewMeta(recordValue);
				}
				if (typeValue != null && !valueReference.Value.Type.Equals(typeValue))
				{
					valueReference = valueReference.Value.NewType(typeValue);
				}
				this.reader.ReadEndValue();
				return valueReference;
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x00036D74 File Offset: 0x00034F74
			private Value ReadPrimitiveValue(ValueKind kind)
			{
				if (kind == ValueKind.Null)
				{
					this.reader.ReadNull();
					return Value.Null;
				}
				if (kind == ValueKind.Logical)
				{
					return LogicalValue.New(this.reader.ReadLogical());
				}
				if (kind != ValueKind.Text)
				{
					throw new InvalidOperationException();
				}
				return TextValue.New(this.reader.ReadText());
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x00036DC8 File Offset: 0x00034FC8
			private RecordValue ReadRecord()
			{
				Keys keys;
				this.reader.ReadStartRecord(out keys);
				IValueReference[] array = new IValueReference[keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ReadValue();
				}
				this.reader.ReadEndRecord();
				return RecordValue.New(keys, array);
			}

			// Token: 0x06001AD9 RID: 6873 RVA: 0x00036E18 File Offset: 0x00035018
			private TableValue ReadTable(TypeValue type)
			{
				Keys keys;
				long num = this.reader.ReadStartTable(out keys);
				TableValue tableValue = new XmlTableValue.Deserializer.VirtualTableValue(this.storage, this.id, this.Position, type, num);
				this.reader.ReadEndTable();
				return tableValue;
			}

			// Token: 0x06001ADA RID: 6874 RVA: 0x00036E58 File Offset: 0x00035058
			private IValueReference ReadReference()
			{
				int num = this.reader.ReadInt32Number();
				return new XmlTableValue.Deserializer.ValueReference(this.storage, num);
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x00036E80 File Offset: 0x00035080
			private RecordValue ReadRow(Keys columns)
			{
				IValueReference[] array = new IValueReference[columns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ReadValue();
				}
				return RecordValue.New(columns, array);
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x00036EB8 File Offset: 0x000350B8
			private TypeValue ReadType()
			{
				ValueKind valueKind;
				bool flag;
				this.reader.ReadStartType(out valueKind, out flag);
				TypeValue typeValue;
				if (valueKind != ValueKind.Any && valueKind != ValueKind.Text)
				{
					if (valueKind != ValueKind.Table)
					{
						throw new InvalidOperationException();
					}
					typeValue = this.ReadTableType();
				}
				else
				{
					typeValue = ValueHelper.PrimitiveType(valueKind, flag);
					flag = false;
				}
				if (flag)
				{
					typeValue = typeValue.Nullable;
				}
				this.reader.ReadEndType();
				return typeValue;
			}

			// Token: 0x06001ADD RID: 6877 RVA: 0x00036F14 File Offset: 0x00035114
			private RecordTypeValue ReadRecordType()
			{
				Keys keys;
				bool flag;
				this.reader.ReadStartRecordType(out keys, out flag);
				TypeValue[] array = new TypeValue[keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ReadValue().Value.AsType;
				}
				this.reader.ReadEndRecordType();
				Value[] array2 = new Value[keys.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = XmlTableValue.FieldDescriptor(array[j]);
				}
				return RecordTypeValue.New(RecordValue.New(keys, array2), flag);
			}

			// Token: 0x06001ADE RID: 6878 RVA: 0x00036FA8 File Offset: 0x000351A8
			private TableTypeValue ReadTableType()
			{
				TableKey[] array;
				this.reader.ReadTableType(out array);
				return TableTypeValue.New(this.ReadRecordType());
			}

			// Token: 0x04000811 RID: 2065
			private IStorage storage;

			// Token: 0x04000812 RID: 2066
			private int id;

			// Token: 0x04000813 RID: 2067
			private StreamValueReader reader;

			// Token: 0x02000299 RID: 665
			private class ValueReference : IValueReference
			{
				// Token: 0x06001ADF RID: 6879 RVA: 0x00036FCD File Offset: 0x000351CD
				public ValueReference(IStorage storage, int id)
				{
					this.storage = storage;
					this.id = id;
				}

				// Token: 0x17000CF7 RID: 3319
				// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00036FE3 File Offset: 0x000351E3
				public bool Evaluated
				{
					get
					{
						return this.value != null;
					}
				}

				// Token: 0x17000CF8 RID: 3320
				// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00036FEE File Offset: 0x000351EE
				public Value Value
				{
					get
					{
						if (this.value == null)
						{
							this.value = XmlTableValue.Deserializer.Deserialize(this.storage, this.id);
						}
						return this.value;
					}
				}

				// Token: 0x04000814 RID: 2068
				private IStorage storage;

				// Token: 0x04000815 RID: 2069
				private int id;

				// Token: 0x04000816 RID: 2070
				private Value value;
			}

			// Token: 0x0200029A RID: 666
			private class VirtualTableValue : TableValue
			{
				// Token: 0x06001AE2 RID: 6882 RVA: 0x00037015 File Offset: 0x00035215
				public VirtualTableValue(IStorage storage, int id, long position, TypeValue type, long continuation)
				{
					this.type = type.AsTableType;
					this.storage = storage;
					this.id = id;
					this.position = position;
					this.continuation = continuation;
				}

				// Token: 0x17000CF9 RID: 3321
				// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00037047 File Offset: 0x00035247
				public override TypeValue Type
				{
					get
					{
						return this.type;
					}
				}

				// Token: 0x17000CFA RID: 3322
				// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0003704F File Offset: 0x0003524F
				public override Keys Columns
				{
					get
					{
						return this.type.ItemType.Fields.Keys;
					}
				}

				// Token: 0x06001AE5 RID: 6885 RVA: 0x00037066 File Offset: 0x00035266
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					using (XmlTableValue.Deserializer deserializer = new XmlTableValue.Deserializer(this.storage, this.id))
					{
						deserializer.Position = this.position;
						while (deserializer.Position < this.continuation)
						{
							RecordValue recordValue = deserializer.ReadRow(this.type.ItemType.Fields.Keys);
							yield return recordValue;
						}
					}
					XmlTableValue.Deserializer deserializer = null;
					yield break;
					yield break;
				}

				// Token: 0x04000817 RID: 2071
				private TableTypeValue type;

				// Token: 0x04000818 RID: 2072
				private IStorage storage;

				// Token: 0x04000819 RID: 2073
				private int id;

				// Token: 0x0400081A RID: 2074
				private long position;

				// Token: 0x0400081B RID: 2075
				private long continuation;
			}
		}
	}
}
