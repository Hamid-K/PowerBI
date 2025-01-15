using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DAC RID: 7596
	public class ValueSerializer
	{
		// Token: 0x0600BC44 RID: 48196 RVA: 0x00261920 File Offset: 0x0025FB20
		private ValueSerializer(IEngine engine, bool suppressTypeExceptions, IGetStackFrameExtendedInfo stackFrameExtendedInfo = null, Action<int, int> rowCountCallback = null, ValueSerializerOptions? options = null)
		{
			this.engine = engine;
			this.suppressTypeExceptions = suppressTypeExceptions;
			this.stackFrameExtendedInfo = stackFrameExtendedInfo;
			this.rowCountCallback = rowCountCallback;
			this.options = options ?? ValueSerializer.DefaultOptions;
			this.builder = new StringBuilder();
			this.types = new Dictionary<ITypeValue, string>(ValueSerializer.typeComparer);
			this.emptyRecordTypes = new Dictionary<ITypeValue, string>(ValueSerializer.typeComparer);
			this.interner = new ValueSerializer.StringInterner(engine);
			this.typesToWrite = new Queue<ITypeValue>();
			this.truncatedTypesToWrite = new Queue<ITypeValue>();
			this.emptyRecordTypesToWrite = new Queue<ITypeValue>();
			this.delayedValuesToWrite = new Dictionary<ValueSerializer.DelayedField, string>();
			this.binaryLengthKey = engine.Keys(new string[] { "Binary.Length" });
			this.isNavigationTable = false;
			this.delayedValueIndex = 0;
		}

		// Token: 0x0600BC45 RID: 48197 RVA: 0x002619FC File Offset: 0x0025FBFC
		public static string SerializePreviewException(IEngine engine, ValueException2 e, IGetStackFrameExtendedInfo stackFrameExtendedInfo)
		{
			string text;
			try
			{
				ValueSerializer valueSerializer = new ValueSerializer(engine, true, stackFrameExtendedInfo, null, null);
				valueSerializer.WritePreviewException(e);
				text = valueSerializer.FinishWriting();
			}
			catch (RuntimeException)
			{
				IRecordValue recordValue;
				if (e.MessageString != null)
				{
					IKeys keys = engine.Keys(new string[] { "Reason", "Message" });
					recordValue = engine.Record(keys, new IValue[]
					{
						engine.Text(e.ReasonString),
						engine.Text(e.MessageString)
					});
				}
				else
				{
					IKeys keys2 = engine.Keys(new string[] { "Reason" });
					recordValue = engine.Record(keys2, new IValue[] { engine.Text(e.ReasonString) });
				}
				text = ValueSerializer.SerializePreviewException(engine, engine.Exception(recordValue), stackFrameExtendedInfo);
			}
			return text;
		}

		// Token: 0x0600BC46 RID: 48198 RVA: 0x00261AD8 File Offset: 0x0025FCD8
		public static string SerializePreviewValue(IEngine engine, IValue value, Action<int, int> rowCountCallback, ValueSerializerOptions? options = null)
		{
			ValueSerializer valueSerializer = new ValueSerializer(engine, false, null, rowCountCallback, options);
			if (value.IsBinary)
			{
				value = engine.ApplyPreviewInference(value, null, null);
			}
			valueSerializer.WritePreviewValue(value);
			return valueSerializer.FinishWriting();
		}

		// Token: 0x17002E69 RID: 11881
		// (get) Token: 0x0600BC47 RID: 48199 RVA: 0x00261B03 File Offset: 0x0025FD03
		private bool BuilderAtCapacity
		{
			get
			{
				return this.builder.Length > 10485760;
			}
		}

		// Token: 0x17002E6A RID: 11882
		// (get) Token: 0x0600BC48 RID: 48200 RVA: 0x00261B17 File Offset: 0x0025FD17
		private int RemainingStructureDepth
		{
			get
			{
				if (!this.options.NestedRecords)
				{
					return 0;
				}
				return this.options.MaxValueDepth - this.valueDepth;
			}
		}

		// Token: 0x0600BC49 RID: 48201 RVA: 0x00261B3A File Offset: 0x0025FD3A
		private void RecordRowCount(int rowCount, int errorRowCount)
		{
			if (this.rowCountCallback != null)
			{
				this.rowCountCallback(rowCount, errorRowCount);
			}
		}

		// Token: 0x0600BC4A RID: 48202 RVA: 0x00261B54 File Offset: 0x0025FD54
		private string FinishWriting()
		{
			if (this.BuilderAtCapacity)
			{
				return this.NewValueTruncationError();
			}
			StringBuilder stringBuilder = this.builder;
			this.builder = new StringBuilder();
			this.interner.ApplyEdits(stringBuilder);
			this.Write("let ");
			this.Write("_v");
			this.Write(" = [");
			HashSet<ITypeValue> hashSet = new HashSet<ITypeValue>(this.types.Comparer);
			bool flag = true;
			for (int i = 0; i < 17; i++)
			{
				ValueSerializer.AllowCustomTypes allowCustomTypes = ValueSerializer.AllowCustomTypes.Yes;
				Queue<ITypeValue> queue = this.typesToWrite;
				this.typesToWrite = new Queue<ITypeValue>();
				if (queue.Count == 0)
				{
					queue = this.truncatedTypesToWrite;
					this.truncatedTypesToWrite = new Queue<ITypeValue>();
					allowCustomTypes = ValueSerializer.AllowCustomTypes.Truncate;
				}
				foreach (KeyValuePair<ValueSerializer.DelayedField, string> keyValuePair in this.delayedValuesToWrite)
				{
					if (this.BuilderAtCapacity)
					{
						return this.NewTypeTruncationError();
					}
					if (!flag)
					{
						this.Write(", ");
					}
					flag = false;
					this.Write(this.EscapedIdentifier(keyValuePair.Value));
					this.Write("=");
					this.WriteLimitedValue(keyValuePair.Key.CreateValue(this.engine));
				}
				this.delayedValuesToWrite.Clear();
				while (queue.Count > 0 || this.emptyRecordTypesToWrite.Count > 0)
				{
					if (this.BuilderAtCapacity)
					{
						return this.NewTypeTruncationError();
					}
					if (!flag)
					{
						this.Write(",");
					}
					flag = false;
					if (queue.Count > 0)
					{
						ITypeValue typeValue = queue.Dequeue();
						string text = this.types[typeValue];
						this.Write(text);
						this.Write("=");
						IRecordValue recordValue = this.GetMeta(typeValue);
						if (hashSet.Add(typeValue))
						{
							if (i == 16)
							{
								this.WriteLimitedType(typeValue);
							}
							else if (recordValue.Keys.Length > 0)
							{
								this.WriteTypeReference(typeValue.SubtractMetaValue.AsType, true, allowCustomTypes);
								this.Write(" meta ");
								if (i > 1)
								{
									recordValue = this.StripFieldDescriptions(recordValue);
								}
								this.WriteMetaRecord(recordValue, false);
							}
							else
							{
								this.WriteCustomType(typeValue, allowCustomTypes);
							}
						}
					}
					else
					{
						ITypeValue typeValue2 = this.emptyRecordTypesToWrite.Dequeue();
						string text2 = this.emptyRecordTypes[typeValue2];
						this.Write(text2);
						this.Write("=");
						this.WriteEmptyRecordForRecordType(typeValue2.AsType.AsRecordType);
					}
				}
			}
			while (this.truncatedTypesToWrite.Count > 0)
			{
				if (!flag)
				{
					this.Write(",");
				}
				flag = false;
				ITypeValue typeValue3 = this.truncatedTypesToWrite.Dequeue();
				string text3 = this.types[typeValue3];
				this.Write(text3);
				this.Write("=");
				if (hashSet.Add(typeValue3))
				{
					this.WriteLimitedType(typeValue3);
				}
			}
			foreach (string text4 in this.interner.GetWriteValues())
			{
				if (!flag)
				{
					this.Write(",");
				}
				flag = false;
				this.Write(text4);
			}
			this.Write("] in ");
			this.Write(stringBuilder.ToString());
			this.interner.ApplyEdits(this.builder);
			return this.builder.ToString();
		}

		// Token: 0x0600BC4B RID: 48203 RVA: 0x00261EDC File Offset: 0x002600DC
		private IRecordValue StripFieldDescriptions(IRecordValue metadata)
		{
			int num;
			if (!this.options.StripFieldDescriptions || !metadata.Keys.TryGetIndex("Documentation.FieldDescription", out num))
			{
				return metadata;
			}
			string[] array = new string[metadata.Keys.Length - 1];
			IValue[] array2 = new IValue[metadata.Keys.Length - 1];
			int num2 = 0;
			for (int i = 0; i < metadata.Keys.Length; i++)
			{
				if (i != num)
				{
					array[num2] = metadata.Keys[i];
					array2[num2] = metadata[i];
					num2++;
				}
			}
			return this.engine.Record(this.engine.Keys(array), array2);
		}

		// Token: 0x0600BC4C RID: 48204 RVA: 0x00261F8A File Offset: 0x0026018A
		private void WriteTruncatedMetaField()
		{
			this.Write("Serializer.Truncated");
			this.Write(" = true");
		}

		// Token: 0x0600BC4D RID: 48205 RVA: 0x00261FA4 File Offset: 0x002601A4
		private void WriteFunctionSignature(IFunctionTypeValue signature, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			if (this.isNavigationTable)
			{
				this.Write("() as any");
				return;
			}
			this.Write("(");
			for (int i = 0; i < signature.ParameterCount; i++)
			{
				if (i != 0)
				{
					this.Write(", ");
				}
				if (i >= signature.Min)
				{
					this.Write("optional ");
					this.Write(this.EscapedIdentifier(signature.ParameterName(i)));
				}
				else if (signature.ParameterName(i) == "optional")
				{
					this.Write("#\"optional\"");
				}
				else
				{
					this.Write(this.EscapedIdentifier(signature.ParameterName(i)));
				}
				ITypeValue typeValue;
				try
				{
					typeValue = signature.ParameterType(i);
				}
				catch (ValueException2)
				{
					if (!this.suppressTypeExceptions)
					{
						throw;
					}
					typeValue = this.engine.Type(TypeHandle.Any);
				}
				this.WriteParameterOrReturnType(typeValue, allowCustomTypes);
			}
			this.Write(")");
			this.WriteParameterOrReturnType(signature.ReturnType, allowCustomTypes);
		}

		// Token: 0x0600BC4E RID: 48206 RVA: 0x002620A8 File Offset: 0x002602A8
		private void WriteParameterOrReturnType(ITypeValue typeValue, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			IRecordValue recordValue = ((allowCustomTypes != ValueSerializer.AllowCustomTypes.No) ? this.GetMeta(typeValue) : null);
			if (recordValue != null && recordValue.Keys.Length > 0)
			{
				this.Write(" as ");
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
			if (!typeValue.Equals(this.engine.Type(TypeHandle.Any)) || allowCustomTypes != ValueSerializer.AllowCustomTypes.No)
			{
				this.Write(" as ");
				this.WriteTypeReference(typeValue, false, allowCustomTypes);
			}
		}

		// Token: 0x0600BC4F RID: 48207 RVA: 0x00262114 File Offset: 0x00260314
		private void WriteFunctionValue(IValue value)
		{
			IFunctionTypeValue asFunctionType = value.Type.AsFunctionType;
			this.Write("(");
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				ValueSerializer.Mark mark = this.NewMark();
				this.WriteFunctionSignature(asFunctionType, ValueSerializer.AllowCustomTypes.No);
				this.Write(" => ...");
				string textBetweenMarks = this.GetTextBetweenMarks(mark, this.NewMark());
				this.Write(") meta [");
				this.Write("Serializer.FunctionText");
				this.Write(" = ");
				this.Write(this.engine.EscapeString(textBetweenMarks));
				if (valueScope.IsTruncated)
				{
					this.Write(", ");
					this.WriteTruncatedMetaField();
				}
				this.Write("]");
			}
		}

		// Token: 0x0600BC50 RID: 48208 RVA: 0x002621E0 File Offset: 0x002603E0
		private void WritePreviewValue(IValue value)
		{
			this.WritePreviewValue(value, this.RemainingStructureDepth);
		}

		// Token: 0x0600BC51 RID: 48209 RVA: 0x002621F0 File Offset: 0x002603F0
		private void WritePreviewValue(IValue value, int serializeStructuredValueDepth)
		{
			IRecordValue meta = this.GetMeta(value);
			this.WriteValueBegin(value, meta);
			using (ValueSerializer.ValueScope scope = this.NewValueScope())
			{
				this.HandleErrors(delegate
				{
					if (scope.ValueDepthExceeded)
					{
						this.WriteLimitedValueScoped(value);
						meta = this.engine.EmptyRecord;
						return;
					}
					if (value.IsRecord)
					{
						this.WritePreviewRecord(value.AsRecord, serializeStructuredValueDepth);
						return;
					}
					if (value.IsBinary)
					{
						this.WriteLimitedBinary(value.AsBinary, this.options.TruncatedBinaryLength);
						return;
					}
					if (value.IsList)
					{
						this.WritePreviewList(value);
						return;
					}
					if (value.IsTable)
					{
						this.WritePreviewTable(value);
						return;
					}
					if (value.IsFunction)
					{
						this.WriteFunctionValue(value);
						return;
					}
					this.WriteTopLevelPrimitive(value);
				});
				this.WriteValueEnd(value, meta, scope.IsTruncated);
			}
		}

		// Token: 0x0600BC52 RID: 48210 RVA: 0x0026229C File Offset: 0x0026049C
		private void WriteValueBegin(IValue value, IRecordValue meta)
		{
			if (this.IsCustomType(value.Type))
			{
				this.Write("Value.ReplaceType(");
			}
		}

		// Token: 0x0600BC53 RID: 48211 RVA: 0x002622B8 File Offset: 0x002604B8
		private IRecordValue GetMeta(IValue value)
		{
			IRecordValue recordValue = value.MetaValue;
			long num;
			if (value.IsBinary && value.AsBinary.TryGetLength(out num))
			{
				IRecordValue recordValue2 = this.engine.Record(this.binaryLengthKey, new IValue[] { this.engine.Number((double)num) });
				recordValue = recordValue.Concatenate(recordValue2).AsRecord;
			}
			return recordValue;
		}

		// Token: 0x0600BC54 RID: 48212 RVA: 0x0026231C File Offset: 0x0026051C
		private void WriteValueEnd(IValue value, IRecordValue meta, bool truncated)
		{
			if (this.IsCustomType(value.Type))
			{
				this.Write(", ");
				this.WriteTypeReference(value.Type, true);
				this.Write(")");
			}
			if (meta.Keys.Length > 0 || truncated)
			{
				this.Write(" meta ");
				this.WriteMetaRecord(meta, truncated);
			}
		}

		// Token: 0x0600BC55 RID: 48213 RVA: 0x00262380 File Offset: 0x00260580
		private void WriteMetaRecord(IRecordValue value, bool truncatedValue)
		{
			IRecordValue meta = this.GetMeta(value);
			this.WriteValueBegin(value, meta);
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.Write("[");
				if (truncatedValue)
				{
					this.WriteTruncatedMetaField();
				}
				int num = 0;
				while (num < value.Keys.Length && !this.BuilderAtCapacity)
				{
					if (num > 0 || truncatedValue)
					{
						this.Write(",");
					}
					this.WriteMetaField(value, num);
					num++;
				}
				this.Write("]");
				this.WriteValueEnd(value, meta, valueScope.IsTruncated);
			}
		}

		// Token: 0x0600BC56 RID: 48214 RVA: 0x0026242C File Offset: 0x0026062C
		private void WriteMetaField(IRecordValue value, int fieldIndex)
		{
			ValueSerializer.Mark mark = this.NewMark();
			string fieldName = value.Keys[fieldIndex];
			this.Write(this.EscapedIdentifier(fieldName));
			this.Write("=");
			this.HandleErrors(delegate
			{
				IRecordTypeValue asRecordType = value.Type.AsRecordType;
				string text;
				string text2;
				bool? flag;
				if (this.IsDelayedField(value, asRecordType, fieldIndex, out text, out text2, out flag))
				{
					this.WriteDelayedField(text, text2, flag);
				}
				else
				{
					IValue value2 = value[fieldIndex];
					if (ValueSerializer.AllowedList.Contains(fieldName))
					{
						this.WriteAllowedListMetadata(fieldName, value2);
					}
					else
					{
						this.WritePreviewValue(value2);
					}
				}
				if (this.BuilderAtCapacity)
				{
					mark.ResetTo();
				}
			});
		}

		// Token: 0x0600BC57 RID: 48215 RVA: 0x002624AC File Offset: 0x002606AC
		private void WriteAllowedListMetadata(string fieldName, IValue fieldValue)
		{
			if (fieldName == "Expression.Stack")
			{
				ISourceBuilder sourceBuilder = this.engine.CreateSourceBuilder();
				sourceBuilder.BeginList();
				foreach (IValueReference2 valueReference in fieldValue.AsList)
				{
					this.WriteStackTraceFrame(sourceBuilder, valueReference.Value.AsRecord);
				}
				sourceBuilder.EndList();
				this.Write(sourceBuilder.ToSource());
				return;
			}
			if (!(fieldName == "Web.Selectors"))
			{
				if (fieldName == "Cube.Hierarchies")
				{
					bool flag = this.serializeTypeMetadata;
					this.serializeTypeMetadata = true;
					try
					{
						this.WritePreviewValue(fieldValue.AsRecord);
						return;
					}
					finally
					{
						this.serializeTypeMetadata = flag;
					}
				}
				throw new InvalidOperationException();
			}
			this.WritePreviewList(fieldValue);
		}

		// Token: 0x0600BC58 RID: 48216 RVA: 0x00262598 File Offset: 0x00260798
		private void WriteStackTraceFrame(ISourceBuilder sb, IRecordValue stackFrame)
		{
			IValue sectionName = this.GetSectionName(stackFrame["Location"]);
			if (sectionName != null)
			{
				sb.BeginRecord();
				if (sectionName != null)
				{
					sb.AddField("Location");
					this.WriteStackFrameLocation(sb, stackFrame["Location"], sectionName);
				}
				sb.EndRecord();
			}
		}

		// Token: 0x0600BC59 RID: 48217 RVA: 0x002625EC File Offset: 0x002607EC
		private IValue GetSectionName(IValue frameLocation)
		{
			if (frameLocation.IsNull)
			{
				return null;
			}
			IValue value = frameLocation.AsRecord["Section"];
			if (!value.IsText)
			{
				return null;
			}
			return value;
		}

		// Token: 0x0600BC5A RID: 48218 RVA: 0x00262620 File Offset: 0x00260820
		private void WriteStackFrameLocation(ISourceBuilder sb, IValue frameLocation, IValue sectionName)
		{
			sb.BeginRecord();
			if (this.stackFrameExtendedInfo != null)
			{
				IRecordValue recordValue = this.stackFrameExtendedInfo.GetStackFrameExtendedInfo(this.engine, frameLocation, sectionName);
				foreach (string text in recordValue.Keys)
				{
					sb.AddField(text);
					sb.Primitive(recordValue[text]);
				}
			}
			sb.AddField("Start");
			this.WritePosition(sb, frameLocation.AsRecord["Start"]);
			sb.AddField("End");
			this.WritePosition(sb, frameLocation.AsRecord["End"]);
			sb.EndRecord();
		}

		// Token: 0x0600BC5B RID: 48219 RVA: 0x002626EC File Offset: 0x002608EC
		private void WritePosition(ISourceBuilder sb, IValue position)
		{
			if (position.IsNull)
			{
				sb.Primitive(this.engine.Null);
				return;
			}
			sb.BeginRecord();
			sb.AddField("Row");
			sb.Primitive(position.AsRecord["Row"]);
			sb.AddField("Column");
			sb.Primitive(position.AsRecord["Column"]);
			sb.EndRecord();
		}

		// Token: 0x0600BC5C RID: 48220 RVA: 0x00262768 File Offset: 0x00260968
		private bool IsCustomType(ITypeValue value)
		{
			if (this.serializeTypeMetadata && value.MetaValue.Keys.Length > 0)
			{
				return true;
			}
			if (value.IsListType)
			{
				if (value.AsListType.ItemType != this.engine.Type(TypeHandle.Any))
				{
					return true;
				}
			}
			else if (value.IsRecordType)
			{
				if (value != this.engine.Type(TypeHandle.Record))
				{
					ITypeValue typeValue = this.engine.Type(TypeHandle.Any);
					IRecordTypeValue asRecordType = value.AsRecordType;
					for (int i = 0; i < asRecordType.Fields.Keys.Length; i++)
					{
						if (asRecordType.Fields[i].AsRecord["Type"] != typeValue)
						{
							return true;
						}
					}
				}
			}
			else if (value.IsFunctionType && value != this.engine.Type(TypeHandle.Function))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600BC5D RID: 48221 RVA: 0x0026283E File Offset: 0x00260A3E
		private void WriteTypeReference(ITypeValue typeValue, bool topLevel)
		{
			this.WriteTypeReference(typeValue, topLevel, ValueSerializer.AllowCustomTypes.Yes);
		}

		// Token: 0x0600BC5E RID: 48222 RVA: 0x0026284C File Offset: 0x00260A4C
		private bool WritePrimitiveType(ITypeValue typeValue, bool topLevel, ValueSerializer.AllowCustomTypes allowCustomTypes, TypeHandle typeHandle, string nullableText, string nonNullableText)
		{
			if (typeValue.Equals(this.engine.Type(typeHandle)))
			{
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue.MetaValue.Keys.Length == 0)
				{
					this.WritePrimitiveType(nullableText, topLevel);
				}
				else
				{
					this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				}
				return true;
			}
			if (typeValue.Equals(this.engine.Type(typeHandle).NonNullable))
			{
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue.MetaValue.Keys.Length == 0)
				{
					this.WritePrimitiveType(nonNullableText, topLevel);
				}
				else
				{
					this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600BC5F RID: 48223 RVA: 0x002628E0 File Offset: 0x00260AE0
		private void WriteTypeReference(ITypeValue typeValue, bool topLevel, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			if (this.WritePrimitiveType(typeValue, topLevel, allowCustomTypes, TypeHandle.Any, "any", "anynonnull") || this.WritePrimitiveType(typeValue, topLevel, allowCustomTypes, TypeHandle.Null, "null", "none"))
			{
				return;
			}
			if (typeValue.IsNullable)
			{
				if (allowCustomTypes != ValueSerializer.AllowCustomTypes.No)
				{
					this.WriteCustomTypeReference(typeValue, allowCustomTypes);
					return;
				}
				if (topLevel)
				{
					this.Write("type ");
					topLevel = false;
				}
				this.Write("nullable ");
				typeValue = typeValue.NonNullable;
			}
			if (allowCustomTypes != ValueSerializer.AllowCustomTypes.No && typeValue.MetaValue.Keys.Length == 0 && this.WriteIfPrimitiveAliasType(typeValue))
			{
				return;
			}
			if (allowCustomTypes != ValueSerializer.AllowCustomTypes.No && typeValue.MetaValue.Keys.Length != 0)
			{
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Text)))
			{
				this.WritePrimitiveType("text", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Binary)))
			{
				this.WritePrimitiveType("binary", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Number)))
			{
				this.WritePrimitiveType("number", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Date)))
			{
				this.WritePrimitiveType("date", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.DateTime)))
			{
				this.WritePrimitiveType("datetime", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.DateTimeZone)))
			{
				this.WritePrimitiveType("datetimezone", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Time)))
			{
				this.WritePrimitiveType("time", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Duration)))
			{
				this.WritePrimitiveType("duration", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Logical)))
			{
				this.WritePrimitiveType("logical", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Action)))
			{
				this.WritePrimitiveType("action", topLevel);
				return;
			}
			if (typeValue.IsCompatibleWith(this.engine.Type(TypeHandle.Type)))
			{
				this.WritePrimitiveType("type", topLevel);
				return;
			}
			if (typeValue.IsFunctionType)
			{
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue == this.engine.Type(TypeHandle.Function))
				{
					this.WritePrimitiveType("function", topLevel);
					return;
				}
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
			else if (typeValue.IsRecordType)
			{
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue == this.engine.Type(TypeHandle.Record))
				{
					this.WritePrimitiveType("record", topLevel);
					return;
				}
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
			else if (typeValue.IsTableType)
			{
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue == this.engine.Type(TypeHandle.Table))
				{
					this.WritePrimitiveType("table", topLevel);
					return;
				}
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
			else
			{
				if (!typeValue.IsListType)
				{
					this.WritePrimitiveType("any", topLevel);
					return;
				}
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.No || typeValue == this.engine.Type(TypeHandle.List))
				{
					this.WritePrimitiveType("list", topLevel);
					return;
				}
				this.WriteCustomTypeReference(typeValue, allowCustomTypes);
				return;
			}
		}

		// Token: 0x0600BC60 RID: 48224 RVA: 0x00262BD8 File Offset: 0x00260DD8
		private bool WriteIfPrimitiveAliasType(ITypeValue typeValue)
		{
			if (typeValue.Equals(this.engine.Type(TypeHandle.Byte)))
			{
				this.WritePrimitiveAliasType("Byte.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Int8)))
			{
				this.WritePrimitiveAliasType("Int8.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Int16)))
			{
				this.WritePrimitiveAliasType("Int16.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Int32)))
			{
				this.WritePrimitiveAliasType("Int32.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Int64)))
			{
				this.WritePrimitiveAliasType("Int64.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Decimal)))
			{
				this.WritePrimitiveAliasType("Decimal.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Currency)))
			{
				this.WritePrimitiveAliasType("Currency.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Percentage)))
			{
				this.WritePrimitiveAliasType("Percentage.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Single)))
			{
				this.WritePrimitiveAliasType("Single.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Double)))
			{
				this.WritePrimitiveAliasType("Double.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Character)))
			{
				this.WritePrimitiveAliasType("Character.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Guid)))
			{
				this.WritePrimitiveAliasType("Guid.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Uri)))
			{
				this.WritePrimitiveAliasType("Uri.Type");
				return true;
			}
			if (typeValue.Equals(this.engine.Type(TypeHandle.Password)))
			{
				this.WritePrimitiveAliasType("Password.Type");
				return true;
			}
			return false;
		}

		// Token: 0x0600BC61 RID: 48225 RVA: 0x00262DBD File Offset: 0x00260FBD
		private void WritePrimitiveAliasType(string typeName)
		{
			this.WritePrimitiveType(typeName, false);
		}

		// Token: 0x0600BC62 RID: 48226 RVA: 0x00262DC7 File Offset: 0x00260FC7
		private void WritePrimitiveType(string name, bool topLevel)
		{
			if (topLevel)
			{
				this.Write("type ");
			}
			this.Write(name);
		}

		// Token: 0x0600BC63 RID: 48227 RVA: 0x00262DE0 File Offset: 0x00260FE0
		private void WriteCustomTypeReference(ITypeValue value, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			string text;
			if (!this.types.TryGetValue(value, out text))
			{
				text = "t" + this.types.Count.ToString();
				this.types.Add(value, text);
				if (allowCustomTypes == ValueSerializer.AllowCustomTypes.Yes)
				{
					this.typesToWrite.Enqueue(value);
				}
				else
				{
					this.truncatedTypesToWrite.Enqueue(value);
				}
			}
			this.Write("@");
			this.Write("_v");
			this.Write("[");
			this.Write(text);
			this.Write("]");
		}

		// Token: 0x0600BC64 RID: 48228 RVA: 0x00262E79 File Offset: 0x00261079
		private void WriteTableTypeReference(ITypeValue value, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			this.WriteCustomTypeReference(value, allowCustomTypes);
		}

		// Token: 0x0600BC65 RID: 48229 RVA: 0x00262E83 File Offset: 0x00261083
		private void WriteLimitedType(ITypeValue typeValue)
		{
			this.Write("type ");
			this.Write(typeValue.ToSource());
		}

		// Token: 0x0600BC66 RID: 48230 RVA: 0x00262E9C File Offset: 0x0026109C
		private void WriteCustomType(ITypeValue typeValue, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			if (typeValue.IsNullable)
			{
				this.Write("type nullable ");
				this.WriteTypeReference(typeValue.NonNullable, false, allowCustomTypes);
				return;
			}
			if (typeValue.IsTableType)
			{
				this.WriteCustomTableType(typeValue.AsTableType, allowCustomTypes);
				return;
			}
			if (typeValue.IsListType)
			{
				this.WriteCustomListType(typeValue.AsListType, allowCustomTypes);
				return;
			}
			if (typeValue.IsRecordType)
			{
				this.WriteCustomRecordType(typeValue.AsRecordType, allowCustomTypes);
				return;
			}
			if (typeValue.IsFunctionType)
			{
				this.WriteCustomFunctionType(typeValue, allowCustomTypes);
				return;
			}
			this.WriteTypeReference(typeValue, true);
		}

		// Token: 0x0600BC67 RID: 48231 RVA: 0x00262F28 File Offset: 0x00261128
		private void WriteCustomFunctionType(ITypeValue type, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			ValueSerializer.Mark mark = this.NewMark();
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.Write("type function");
				if (type != this.engine.Type(TypeHandle.Function))
				{
					this.Write(" ");
					this.WriteFunctionSignature(type.AsFunctionType, allowCustomTypes);
					if (valueScope.IsTruncated)
					{
						mark.ResetTo();
						this.Write("(");
						this.Write("type function");
						this.Write(" ");
						this.WriteFunctionSignature(type.AsFunctionType, allowCustomTypes);
						this.Write(") meta [");
						this.WriteTruncatedMetaField();
						this.Write("]");
					}
					if (this.BuilderAtCapacity)
					{
						mark.ResetTo();
						this.Write(this.NewTypeTruncationError());
					}
				}
			}
		}

		// Token: 0x0600BC68 RID: 48232 RVA: 0x00263010 File Offset: 0x00261210
		private void WriteCustomRecordType(IRecordTypeValue recordType, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			ValueSerializer.Mark mark = this.NewMark();
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.Write("type [");
				IRecordValue fields = recordType.Fields;
				int i = 0;
				while (i < fields.Keys.Length)
				{
					if (this.BuilderAtCapacity)
					{
						mark.ResetTo();
						this.Write(this.NewTypeTruncationError());
						return;
					}
					if (i > 0)
					{
						this.Write(",");
					}
					IRecordValue asRecord = fields[i].AsRecord;
					if (asRecord["Optional"].AsBoolean)
					{
						this.Write("optional ");
						this.Write(this.EscapedIdentifier(fields.Keys[i]));
					}
					else
					{
						this.Write(this.EscapedIdentifier(fields.Keys[i], ValueSerializer.FieldNameContext));
					}
					this.Write("=");
					ITypeValue typeValue;
					try
					{
						typeValue = asRecord["Type"].AsType;
					}
					catch (ValueException2)
					{
						if (!this.suppressTypeExceptions)
						{
							throw;
						}
						typeValue = this.engine.Type(TypeHandle.Any);
					}
					ValueSerializer.AllowCustomTypes allowCustomTypes2 = allowCustomTypes;
					IValue value;
					if (!typeValue.TryGetMetaField("NavigationTable.ItemKind", out value))
					{
						goto IL_013E;
					}
					if (allowCustomTypes2 == ValueSerializer.AllowCustomTypes.Yes)
					{
						allowCustomTypes2 = ValueSerializer.AllowCustomTypes.Truncate;
						goto IL_013E;
					}
					if (typeValue.IsRecordType)
					{
						this.Write("[]");
					}
					else
					{
						if (!typeValue.IsTableType)
						{
							goto IL_013E;
						}
						this.Write("table[]");
					}
					IL_0149:
					i++;
					continue;
					IL_013E:
					this.WriteTypeReference(typeValue, false, allowCustomTypes2);
					goto IL_0149;
				}
				if (recordType.Open)
				{
					if (fields.Keys.Length > 0)
					{
						this.Write(",");
					}
					this.Write("...");
				}
				this.Write("]");
				if (valueScope.IsTruncated)
				{
					this.Write(" meta [");
					this.WriteTruncatedMetaField();
					this.Write("]");
				}
			}
		}

		// Token: 0x0600BC69 RID: 48233 RVA: 0x0026321C File Offset: 0x0026141C
		private void WriteCustomTableType(ITableTypeValue tableType, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			this.Write("type table ");
			IRecordTypeValue itemType = tableType.ItemType;
			if (itemType == this.engine.Type(TypeHandle.Record))
			{
				this.Write("[]");
				return;
			}
			this.WriteTypeReference(itemType, false, allowCustomTypes);
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x00263260 File Offset: 0x00261460
		private void WriteCustomListType(IListTypeValue listType, ValueSerializer.AllowCustomTypes allowCustomTypes)
		{
			this.Write("type {");
			ITypeValue itemType = listType.ItemType;
			this.WriteTypeReference(itemType, false, allowCustomTypes);
			this.Write("}");
		}

		// Token: 0x0600BC6B RID: 48235 RVA: 0x00263294 File Offset: 0x00261494
		private void WriteLimitedRecord(IRecordValue value)
		{
			IRecordValue meta = this.GetMeta(value);
			this.WriteValueBegin(value, meta);
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.HandleErrors(delegate
				{
					this.WritePreviewRecord(value, 0);
				});
				this.WriteValueEnd(value, meta, valueScope.IsTruncated);
			}
		}

		// Token: 0x0600BC6C RID: 48236 RVA: 0x0026331C File Offset: 0x0026151C
		private void WriteLimitedValue(IValue value)
		{
			IRecordValue recordValue = (value.IsBinary ? this.engine.Record(this.engine.Keys(Array.Empty<string>()), Array.Empty<IValue>()) : this.GetMeta(value));
			this.WriteValueBegin(value, recordValue);
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.HandleErrors(delegate
				{
					this.WriteLimitedValueScoped(value);
				});
				this.WriteValueEnd(value, recordValue, valueScope.IsTruncated);
			}
		}

		// Token: 0x0600BC6D RID: 48237 RVA: 0x002633D4 File Offset: 0x002615D4
		private void WriteLimitedValueScoped(IValue value)
		{
			if (value.IsRecord)
			{
				this.WriteEmptyRecord(value.AsRecord);
				return;
			}
			if (value.IsBinary)
			{
				this.WriteLimitedBinary(value.AsBinary, this.options.TruncatedBinaryLength);
				return;
			}
			if (value.IsList)
			{
				this.WriteEmptyList(value);
				return;
			}
			if (value.IsTable)
			{
				this.WriteEmptyTable(value);
				return;
			}
			if (value.IsFunction)
			{
				this.WriteFunctionValue(value);
				return;
			}
			this.WriteLimitedPrimitive(value);
		}

		// Token: 0x0600BC6E RID: 48238 RVA: 0x00263450 File Offset: 0x00261650
		private void WriteLimitedPrimitive(IValue value)
		{
			if (value.IsType)
			{
				this.WriteTypeReference(value.AsType, true);
				return;
			}
			if (value.IsText)
			{
				string text = value.ToString();
				if (text.Length > 1024)
				{
					this.NoteTruncation();
					text = text.Substring(0, 1024 - "...".Length) + "...";
				}
				this.interner.WriteText(text, this.builder);
				return;
			}
			this.Write(value.SubtractMetaValue.ToSource());
		}

		// Token: 0x0600BC6F RID: 48239 RVA: 0x002634DC File Offset: 0x002616DC
		private void WriteTopLevelPrimitive(IValue value)
		{
			if (value.IsType)
			{
				this.WriteTypeReference(value.AsType, true);
				return;
			}
			if (value.IsText)
			{
				string text = value.ToString();
				if (text.Length > 10485632)
				{
					this.NoteTruncation();
					text = text.Substring(0, 10485632 - "...".Length) + "...";
				}
				this.interner.WriteText(text, this.builder);
				return;
			}
			this.Write(value.SubtractMetaValue.ToSource());
		}

		// Token: 0x0600BC70 RID: 48240 RVA: 0x00263568 File Offset: 0x00261768
		private void WriteEmptyRecord(IRecordValue value)
		{
			IRecordTypeValue asRecordType = value.Type.AsRecordType;
			string text;
			if (!this.emptyRecordTypes.TryGetValue(asRecordType, out text))
			{
				text = "v" + this.emptyRecordTypes.Count.ToString();
				this.emptyRecordTypes.Add(asRecordType, text);
				this.emptyRecordTypesToWrite.Enqueue(asRecordType);
			}
			this.Write("@");
			this.Write("_v");
			this.Write("[");
			this.Write(text);
			this.Write("]");
		}

		// Token: 0x0600BC71 RID: 48241 RVA: 0x002635FC File Offset: 0x002617FC
		private void WriteEmptyRecordForRecordType(IRecordTypeValue recordType)
		{
			IKeys keys = recordType.Fields.Keys;
			List<string> list = new List<string>();
			List<IValue> list2 = new List<IValue>();
			for (int i = 0; i < keys.Length; i++)
			{
				if (!recordType.Fields[keys[i]].AsRecord["Optional"].AsBoolean)
				{
					IValue @null = this.engine.Null;
					list.Add(keys[i]);
					list2.Add(@null);
				}
			}
			IRecordValue recordValue = this.engine.Record(this.engine.Keys(list.ToArray()), list2.ToArray());
			using (ValueSerializer.ValueScope valueScope = this.NewValueScope())
			{
				this.WritePreviewRecord(recordValue, 0);
				if (valueScope.IsTruncated)
				{
					this.Write(" meta [");
					this.WriteTruncatedMetaField();
					this.Write("]");
				}
			}
		}

		// Token: 0x0600BC72 RID: 48242 RVA: 0x002636FC File Offset: 0x002618FC
		private void WriteEmptyList(IValue value)
		{
			this.Write("{}");
		}

		// Token: 0x0600BC73 RID: 48243 RVA: 0x00263709 File Offset: 0x00261909
		private void WriteEmptyTable(IValue value)
		{
			this.Write("Table.FromRows({},");
			this.WriteTypeReference(value.Type, true);
			this.Write(")");
		}

		// Token: 0x0600BC74 RID: 48244 RVA: 0x00263730 File Offset: 0x00261930
		private void WriteLimitedBinary(IBinaryValue value, int maxLength)
		{
			this.Write("Binary.FromText(\"");
			if (maxLength == 0)
			{
				this.Write("\")");
				return;
			}
			using (Stream stream = value.Open().Take((long)(maxLength + 1)))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					this.Write(Convert.ToBase64String(memoryStream.GetBuffer(), 0, Math.Min((int)memoryStream.Length, maxLength)));
					this.Write("\")");
					if (memoryStream.Length > (long)maxLength)
					{
						this.Write(" meta [");
						this.WriteTruncatedMetaField();
						this.Write("]");
					}
				}
			}
		}

		// Token: 0x0600BC75 RID: 48245 RVA: 0x002637F8 File Offset: 0x002619F8
		private void WritePreviewList(IValue value)
		{
			this.Write("{");
			bool first = true;
			this.HandleErrors(delegate
			{
				int num = 1000;
				int num2 = 0;
				int num3 = 0;
				using (IEnumerator<IValueReference2> enumerator = value.AsList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IValueReference2 valueReference = enumerator.Current;
						ValueSerializer.Mark mark = this.NewMark();
						this.haveError = false;
						if (num-- == 0)
						{
							this.NoteTruncation();
							break;
						}
						if (!first)
						{
							this.Write(",");
						}
						this.HandleErrors(delegate
						{
							IValue value2 = valueReference.Value;
							if (value2.IsRecord)
							{
								this.WriteLimitedRecord(value2.AsRecord);
								return;
							}
							this.WriteLimitedValue(value2);
						});
						if (this.BuilderAtCapacity)
						{
							mark.ResetTo();
							this.NoteTruncation();
							break;
						}
						first = false;
						num2++;
						if (this.haveError)
						{
							num3++;
						}
						this.RecordRowCount(num2, num3);
					}
				}
			});
			this.Write("}");
		}

		// Token: 0x0600BC76 RID: 48246 RVA: 0x00263848 File Offset: 0x00261A48
		private void WritePreviewTable(IValue value)
		{
			List<string> list = new List<string>(value.Type.KeyColumnNames);
			if (list.Count > 0)
			{
				this.Write("Table.AddKey(");
			}
			this.Write("Table.FromRows({");
			this.HandleErrors(delegate
			{
				IRecordTypeValue rowType = value.Type.AsTableType.ItemType;
				int fieldCount = rowType.Fields.Keys.Length;
				bool first = true;
				ValueSerializer.NavigationTableInfo? navigationTableInfo;
				this.isNavigationTable = this.IsNavigationTable(value.Type.AsTableType, out navigationTableInfo);
				int rowLimit = 1000;
				int rowCount = 0;
				int errorRowCount = 0;
				this.HandleErrors(delegate
				{
					using (IEnumerator<IValueReference2> enumerator = value.AsTable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IValueReference2 current = enumerator.Current;
							ValueSerializer.Mark mark = this.NewMark();
							this.haveError = false;
							int num = rowLimit;
							rowLimit = num - 1;
							if (num == 0)
							{
								this.NoteTruncation();
								break;
							}
							if (!first)
							{
								this.Write(",");
							}
							this.HandleErrors(delegate
							{
								IRecordValue asRecord = current.Value.AsRecord;
								this.Write("{");
								for (int i = 0; i < fieldCount; i++)
								{
									if (i > 0)
									{
										this.Write(",");
									}
									this.WritePreviewFieldValue(asRecord, i, this.RemainingStructureDepth, navigationTableInfo, rowType);
								}
								this.Write("}");
							});
							if (this.BuilderAtCapacity)
							{
								mark.ResetTo();
								this.NoteTruncation();
								break;
							}
							first = false;
							num = rowCount;
							rowCount = num + 1;
							if (this.haveError)
							{
								num = errorRowCount;
								errorRowCount = num + 1;
							}
							this.RecordRowCount(rowCount, errorRowCount);
						}
					}
				});
			});
			this.Write("},");
			this.WriteTableTypeReference(value.Type, ValueSerializer.AllowCustomTypes.Yes);
			this.Write(")");
			if (list.Count > 0)
			{
				this.Write(",{");
				this.Write(string.Join(",", list.Select((string s) => this.engine.EscapeString(s)).ToArray<string>()));
				this.Write("},true)");
			}
		}

		// Token: 0x0600BC77 RID: 48247 RVA: 0x00263920 File Offset: 0x00261B20
		private bool IsNavigationTable(ITableTypeValue type, out ValueSerializer.NavigationTableInfo? navigationTableInfo)
		{
			navigationTableInfo = null;
			IValue value;
			bool flag = type.TryGetMetaField("NavigationTable.NameColumn", out value) && value.IsText && type.TryGetMetaField("NavigationTable.DataColumn", out value) && value.IsText;
			if (flag)
			{
				string asString = value.AsString;
				string text = null;
				string text2 = null;
				string text3 = null;
				if (type.TryGetMetaField("NavigationTable.ItemKindColumn", out value) && value.IsText)
				{
					text = value.AsString;
				}
				if (type.TryGetMetaField("Preview.DelayColumn", out value) && value.IsText)
				{
					text2 = value.AsString;
				}
				if (type.TryGetMetaField("NavigationTable.IsLeafColumn", out value) && value.IsText)
				{
					text3 = value.AsString;
				}
				navigationTableInfo = new ValueSerializer.NavigationTableInfo?(new ValueSerializer.NavigationTableInfo(asString, text, text2, text3));
			}
			return flag;
		}

		// Token: 0x0600BC78 RID: 48248 RVA: 0x002639E4 File Offset: 0x00261BE4
		private void WritePreviewRecord(IRecordValue value, int serializeStructuredValueDepth = 0)
		{
			this.Write("[");
			this.WritePreviewRecordFields(value, serializeStructuredValueDepth);
			this.Write("]");
		}

		// Token: 0x0600BC79 RID: 48249 RVA: 0x00263A04 File Offset: 0x00261C04
		private void WritePreviewRecordFields(IRecordValue value, int serializeStructuredValueDepth)
		{
			for (int i = 0; i < value.Keys.Length; i++)
			{
				if (i > 0)
				{
					this.Write(",");
				}
				this.WritePreviewField(value, i, serializeStructuredValueDepth);
			}
		}

		// Token: 0x0600BC7A RID: 48250 RVA: 0x00263A40 File Offset: 0x00261C40
		private bool IsDelayedField(IRecordTypeValue expectedType, IRecordValue value, int fieldIndex, ValueSerializer.NavigationTableInfo? navigationTableInfo, out string itemKind, out string typeName, out bool? isLeaf)
		{
			if (expectedType != null && this.IsDelayedField(value, expectedType, fieldIndex, out itemKind, out typeName, out isLeaf))
			{
				return true;
			}
			int num;
			IValue value2;
			IValue value3;
			IValue value4;
			if (navigationTableInfo != null && value.Keys.TryGetIndex(navigationTableInfo.Value.DataColumnName, out num) && num == fieldIndex && navigationTableInfo.Value.ItemKindColumnName != null && navigationTableInfo.Value.PreviewDelayColumnName != null && navigationTableInfo.Value.IsLeafColumnName != null && value.TryGetValue(navigationTableInfo.Value.ItemKindColumnName, out value2) && value.TryGetValue(navigationTableInfo.Value.PreviewDelayColumnName, out value3) && value.TryGetValue(navigationTableInfo.Value.IsLeafColumnName, out value4) && value2.IsText && value3.IsText && value4.IsLogical)
			{
				isLeaf = new bool?(value4.AsBoolean);
				this.writeAllLinks = true;
				itemKind = value2.AsString;
				typeName = value3.AsString;
				return true;
			}
			return this.IsDelayedField(value, value.Type.AsRecordType, fieldIndex, out itemKind, out typeName, out isLeaf);
		}

		// Token: 0x0600BC7B RID: 48251 RVA: 0x00263B80 File Offset: 0x00261D80
		private bool IsDelayedField(IRecordValue value, IRecordTypeValue recordType, int fieldIndex, out string itemKind, out string typeName, out bool? isLeaf)
		{
			if (fieldIndex < recordType.Fields.Keys.Length)
			{
				IValue value2 = recordType.Fields[fieldIndex].AsRecord["Type"];
				IValue value3;
				if (value2.TryGetMetaField("Preview.Delay", out value3) && value3.IsText)
				{
					typeName = value3.AsString;
					isLeaf = null;
					IValue value4;
					itemKind = ((value2.TryGetMetaField("NavigationTable.ItemKind", out value4) && value4.IsText) ? value4.AsString : null);
					IValue value5;
					IValue value6;
					IValue value7;
					if ((value2.TryGetMetaField("NavigationTable.RowConfigurationColumn", out value5) && value5.IsText && value.TryGetValue(value5.AsString, out value6) && value6.TryGetMetaField("NavigationTable.IsLeaf", out value7) && value7.IsLogical) || (value2.TryGetMetaField("NavigationTable.IsLeaf", out value7) && value7.IsLogical))
					{
						isLeaf = new bool?(value7.AsBoolean);
					}
					return true;
				}
			}
			itemKind = null;
			typeName = null;
			isLeaf = null;
			return false;
		}

		// Token: 0x0600BC7C RID: 48252 RVA: 0x00263C90 File Offset: 0x00261E90
		private bool IsInlineField(IRecordTypeValue recordType, int fieldIndex)
		{
			IValue value;
			return recordType != null && fieldIndex < recordType.Fields.Keys.Length && (recordType.Fields[fieldIndex].AsRecord["Type"].TryGetMetaField("Inline", out value) && value.IsLogical) && value.AsBoolean;
		}

		// Token: 0x0600BC7D RID: 48253 RVA: 0x00263CF0 File Offset: 0x00261EF0
		private void WriteDelayedField(string itemKind, string typeName, bool? isLeaf)
		{
			ValueSerializer.DelayedField delayedField = new ValueSerializer.DelayedField(itemKind, typeName, isLeaf);
			if (this.writeAllLinks || this.valueDepth > 1)
			{
				IValue value = delayedField.CreateValue(this.engine);
				this.WriteLimitedValue(value);
				return;
			}
			string text;
			if (!this.delayedValuesToWrite.TryGetValue(delayedField, out text))
			{
				text = "d" + this.delayedValueIndex.ToString();
				this.delayedValueIndex++;
				this.delayedValuesToWrite[delayedField] = text;
			}
			this.Write("@");
			this.Write("_v");
			this.Write("[");
			this.Write(this.EscapedIdentifier(text));
			this.Write("]");
		}

		// Token: 0x0600BC7E RID: 48254 RVA: 0x00263DA8 File Offset: 0x00261FA8
		private void WritePreviewField(IRecordValue value, int fieldIndex, int serializeStructuredValueDepth)
		{
			string text = value.Keys[fieldIndex];
			this.Write(this.EscapedIdentifier(text));
			this.Write("=");
			this.WritePreviewFieldValue(value, fieldIndex, serializeStructuredValueDepth, null, null);
		}

		// Token: 0x0600BC7F RID: 48255 RVA: 0x00263DF0 File Offset: 0x00261FF0
		private void WritePreviewFieldValue(IRecordValue value, int fieldIndex, int serializeStructuredValueDepth, ValueSerializer.NavigationTableInfo? navigationTableInfo = null, IRecordTypeValue expectedType = null)
		{
			this.HandleErrors(delegate
			{
				if (this.IsNavigationTableField(value, fieldIndex))
				{
					this.Write("Table.FromRows({})");
					return;
				}
				string text;
				string text2;
				bool? flag;
				if (this.IsDelayedField(expectedType, value, fieldIndex, navigationTableInfo, out text, out text2, out flag))
				{
					if (!this.TryWriteDimensionTableType(value, fieldIndex))
					{
						this.WriteDelayedField(text, text2, flag);
						return;
					}
				}
				else
				{
					if (this.IsInlineField(expectedType, fieldIndex))
					{
						this.WritePreviewValue(value[fieldIndex]);
						return;
					}
					if (serializeStructuredValueDepth > 0 && ValueSerializer.IsStructuredValue(value[fieldIndex]))
					{
						this.WritePreviewValue(value[fieldIndex], serializeStructuredValueDepth - 1);
						return;
					}
					this.WriteLimitedValue(value[fieldIndex]);
				}
			});
		}

		// Token: 0x0600BC80 RID: 48256 RVA: 0x00263E41 File Offset: 0x00262041
		private static bool IsStructuredValue(IValue value)
		{
			return value.IsRecord || value.IsTable || value.IsList;
		}

		// Token: 0x0600BC81 RID: 48257 RVA: 0x00263E60 File Offset: 0x00262060
		private bool TryWriteDimensionTableType(IRecordValue value, int fieldIndex)
		{
			IValue value2;
			if (value.TryGetValue("Kind", out value2) && value2.IsText && value2.AsString == "Dimension")
			{
				IValue value3 = value[fieldIndex];
				if (value3.IsTable)
				{
					this.Write("Table.FromRows({},");
					this.WriteTableTypeReference(value3.Type, ValueSerializer.AllowCustomTypes.Yes);
					this.Write(")");
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BC82 RID: 48258 RVA: 0x00263ECC File Offset: 0x002620CC
		private bool IsNavigationTableField(IRecordValue value, int fieldIndex)
		{
			if (fieldIndex < value.Keys.Length)
			{
				string text = value.Keys[fieldIndex];
				IValue value2;
				return value.TryGetMetaField("NavigationTable", out value2) && value2.IsText && value2.AsString == text;
			}
			return false;
		}

		// Token: 0x0600BC83 RID: 48259 RVA: 0x00263F1C File Offset: 0x0026211C
		private void WritePreviewException(ValueException2 e)
		{
			this.haveError = true;
			IRecordValue value = e.Value2;
			this.Write("error [");
			bool flag = true;
			int num;
			if (value.Keys.TryGetIndex("Reason", out num))
			{
				this.WritePreviewExceptionField(value, "Reason", 0);
				flag = false;
			}
			if (value.Keys.TryGetIndex("Message", out num))
			{
				if (!flag)
				{
					this.Write(",");
				}
				this.WritePreviewExceptionField(value, "Message", 0);
				flag = false;
			}
			int length = value.Keys.Length;
			for (int i = 0; i < length; i++)
			{
				ValueSerializer.Mark mark = this.NewMark();
				string text = value.Keys[i];
				if (!(text == "Reason") && !(text == "Message"))
				{
					if (i > 0)
					{
						flag = false;
					}
					if (!flag)
					{
						this.Write(",");
					}
					this.WritePreviewExceptionField(value, text, 1);
					if (this.BuilderAtCapacity)
					{
						mark.ResetTo();
						break;
					}
				}
			}
			this.Write("]");
			int num2;
			if (value.MetaValue.Keys.TryGetIndex("Expression.Stack", out num2))
			{
				this.Write(" meta [");
				using (this.NewValueScope())
				{
					this.WriteMetaField(value.MetaValue, num2);
				}
				this.Write("]");
			}
		}

		// Token: 0x0600BC84 RID: 48260 RVA: 0x00264088 File Offset: 0x00262288
		private void WritePreviewExceptionField(IRecordValue error, string field, int serializeStructuredValueDepth = 0)
		{
			ValueSerializer.Mark mark = this.NewMark();
			this.Write(this.engine.EscapeIdentifier(field));
			this.Write("=");
			this.HandleErrors(delegate
			{
				IValue value;
				if (error.TryGetValue(field, out value))
				{
					this.WritePreviewValue(value, serializeStructuredValueDepth);
					return;
				}
				mark.ResetTo();
			}, delegate(ValueException2 e)
			{
				this.Write("...");
			});
		}

		// Token: 0x0600BC85 RID: 48261 RVA: 0x00264103 File Offset: 0x00262303
		private void Write(string s)
		{
			this.builder.Append(s);
		}

		// Token: 0x0600BC86 RID: 48262 RVA: 0x00264112 File Offset: 0x00262312
		private string TruncatedIdentifier(string s)
		{
			if (s.Length > 32767)
			{
				this.NoteTruncation();
				s = s.Substring(0, 32767 - "...".Length) + "...";
			}
			return s;
		}

		// Token: 0x0600BC87 RID: 48263 RVA: 0x0026414B File Offset: 0x0026234B
		private string EscapedIdentifier(string s)
		{
			return this.engine.EscapeIdentifier(this.TruncatedIdentifier(s));
		}

		// Token: 0x0600BC88 RID: 48264 RVA: 0x0026415F File Offset: 0x0026235F
		private string EscapedIdentifier(string s, ContextualKeyword[] keywords)
		{
			return this.engine.EscapeIdentifier(this.TruncatedIdentifier(s), keywords);
		}

		// Token: 0x0600BC89 RID: 48265 RVA: 0x00264174 File Offset: 0x00262374
		private ValueSerializer.ValueScope NewValueScope()
		{
			ValueSerializer.ValueScope valueScope = new ValueSerializer.ValueScope(this);
			this.truncated = false;
			return valueScope;
		}

		// Token: 0x0600BC8A RID: 48266 RVA: 0x00264183 File Offset: 0x00262383
		private void NoteTruncation()
		{
			this.truncated = true;
		}

		// Token: 0x0600BC8B RID: 48267 RVA: 0x0026418C File Offset: 0x0026238C
		private void HandleErrors(Action action)
		{
			this.HandleErrors(action, new Action<ValueException2>(this.WritePreviewException));
		}

		// Token: 0x0600BC8C RID: 48268 RVA: 0x002641A4 File Offset: 0x002623A4
		private void HandleErrors(Action action, Action<ValueException2> handler)
		{
			ValueSerializer.Mark mark = this.NewMark();
			try
			{
				action();
			}
			catch (ValueException2 valueException)
			{
				mark.ResetTo();
				handler(valueException);
			}
		}

		// Token: 0x0600BC8D RID: 48269 RVA: 0x002641E4 File Offset: 0x002623E4
		private ValueSerializer.Mark NewMark()
		{
			return new ValueSerializer.Mark(this.interner, this.builder);
		}

		// Token: 0x0600BC8E RID: 48270 RVA: 0x002641F7 File Offset: 0x002623F7
		private string GetTextBetweenMarks(ValueSerializer.Mark start, ValueSerializer.Mark end)
		{
			return this.builder.ToString(start.Index, end.Index - start.Index);
		}

		// Token: 0x0600BC8F RID: 48271 RVA: 0x0026421A File Offset: 0x0026241A
		private string NewTypeTruncationError()
		{
			return "error [Reason=\"Preview.Error\", Message=\"" + string.Format(CultureInfo.CurrentCulture, Strings.ValueSerializer_TypeTooComplex, Array.Empty<object>()) + "\"]";
		}

		// Token: 0x0600BC90 RID: 48272 RVA: 0x0026423F File Offset: 0x0026243F
		private string NewValueTruncationError()
		{
			return "error [Reason=\"Preview.Error\", Message=\"" + string.Format(CultureInfo.CurrentCulture, Strings.ValueSerializer_ValueTooComplex, Array.Empty<object>()) + "\"]";
		}

		// Token: 0x04005FD8 RID: 24536
		private const int MB = 1048576;

		// Token: 0x04005FD9 RID: 24537
		public const int MaxRows = 1000;

		// Token: 0x04005FDA RID: 24538
		public const int MaxTextLength = 1024;

		// Token: 0x04005FDB RID: 24539
		public const int MaxTopLevelTextLength = 10485632;

		// Token: 0x04005FDC RID: 24540
		public const int MaxIdentifierLength = 32767;

		// Token: 0x04005FDD RID: 24541
		public const int MaxSerializerCapacity = 10485760;

		// Token: 0x04005FDE RID: 24542
		public const int DefaultMaxValueDepth = 3;

		// Token: 0x04005FDF RID: 24543
		public const int MaxExceptionDepth = 1;

		// Token: 0x04005FE0 RID: 24544
		public const string TruncationSuffix = "...";

		// Token: 0x04005FE1 RID: 24545
		public static readonly ValueSerializerOptions DefaultOptions = new ValueSerializerOptions
		{
			MaxValueDepth = 3,
			NestedRecords = false,
			TruncatedBinaryLength = 0,
			StripFieldDescriptions = true
		};

		// Token: 0x04005FE2 RID: 24546
		private static readonly ContextualKeyword[] FieldNameContext = new ContextualKeyword[1];

		// Token: 0x04005FE3 RID: 24547
		private static readonly HashSet<string> AllowedList = new HashSet<string>(new string[] { "Expression.Stack", "Web.Selectors", "Cube.Hierarchies" });

		// Token: 0x04005FE4 RID: 24548
		private static readonly IEqualityComparer<ITypeValue> typeComparer = new ValueSerializer.TypeComparer();

		// Token: 0x04005FE5 RID: 24549
		private const string ExpressionStack = "Expression.Stack";

		// Token: 0x04005FE6 RID: 24550
		private const string CubeHierarchies = "Cube.Hierarchies";

		// Token: 0x04005FE7 RID: 24551
		private const string SerializerFunctionText = "Serializer.FunctionText";

		// Token: 0x04005FE8 RID: 24552
		private const string SerializerTruncated = "Serializer.Truncated";

		// Token: 0x04005FE9 RID: 24553
		private const string StackFrameLocation = "Location";

		// Token: 0x04005FEA RID: 24554
		private const string LocationSection = "Section";

		// Token: 0x04005FEB RID: 24555
		private const string LocationStart = "Start";

		// Token: 0x04005FEC RID: 24556
		private const string LocationEnd = "End";

		// Token: 0x04005FED RID: 24557
		private const string PositionRow = "Row";

		// Token: 0x04005FEE RID: 24558
		private const string PositionColumn = "Column";

		// Token: 0x04005FEF RID: 24559
		private const string ErrorReason = "Reason";

		// Token: 0x04005FF0 RID: 24560
		private const string ErrorMessage = "Message";

		// Token: 0x04005FF1 RID: 24561
		private const string DimensionKind = "Dimension";

		// Token: 0x04005FF2 RID: 24562
		private readonly IEngine engine;

		// Token: 0x04005FF3 RID: 24563
		private readonly bool suppressTypeExceptions;

		// Token: 0x04005FF4 RID: 24564
		private readonly IGetStackFrameExtendedInfo stackFrameExtendedInfo;

		// Token: 0x04005FF5 RID: 24565
		private readonly ValueSerializerOptions options;

		// Token: 0x04005FF6 RID: 24566
		private StringBuilder builder;

		// Token: 0x04005FF7 RID: 24567
		private const string valuesRecordName = "_v";

		// Token: 0x04005FF8 RID: 24568
		private Dictionary<ITypeValue, string> types;

		// Token: 0x04005FF9 RID: 24569
		private Dictionary<ITypeValue, string> emptyRecordTypes;

		// Token: 0x04005FFA RID: 24570
		private ValueSerializer.StringInterner interner;

		// Token: 0x04005FFB RID: 24571
		private int valueDepth;

		// Token: 0x04005FFC RID: 24572
		private Queue<ITypeValue> typesToWrite;

		// Token: 0x04005FFD RID: 24573
		private Queue<ITypeValue> truncatedTypesToWrite;

		// Token: 0x04005FFE RID: 24574
		private Queue<ITypeValue> emptyRecordTypesToWrite;

		// Token: 0x04005FFF RID: 24575
		private Dictionary<ValueSerializer.DelayedField, string> delayedValuesToWrite;

		// Token: 0x04006000 RID: 24576
		private int delayedValueIndex;

		// Token: 0x04006001 RID: 24577
		private bool writeAllLinks;

		// Token: 0x04006002 RID: 24578
		private IKeys binaryLengthKey;

		// Token: 0x04006003 RID: 24579
		private readonly Action<int, int> rowCountCallback;

		// Token: 0x04006004 RID: 24580
		private bool haveError;

		// Token: 0x04006005 RID: 24581
		private bool truncated;

		// Token: 0x04006006 RID: 24582
		private bool isNavigationTable;

		// Token: 0x04006007 RID: 24583
		private bool serializeTypeMetadata;

		// Token: 0x02001DAD RID: 7597
		private sealed class TypeComparer : IEqualityComparer<ITypeValue>
		{
			// Token: 0x0600BC92 RID: 48274 RVA: 0x002642DC File Offset: 0x002624DC
			public bool Equals(ITypeValue x, ITypeValue y)
			{
				if (x.MetaValue.Keys.Length == 0 && y.MetaValue.Keys.Length == 0)
				{
					return x.Equals(y);
				}
				return x == y;
			}

			// Token: 0x0600BC93 RID: 48275 RVA: 0x0026430E File Offset: 0x0026250E
			public int GetHashCode(ITypeValue value)
			{
				if (value.MetaValue.Keys.Length == 0)
				{
					return value.GetHashCode();
				}
				return RuntimeHelpers.GetHashCode(value);
			}
		}

		// Token: 0x02001DAE RID: 7598
		private static class RecordTypeValue
		{
			// Token: 0x04006008 RID: 24584
			public const string TypeKey = "Type";

			// Token: 0x04006009 RID: 24585
			public const string OptionalKey = "Optional";
		}

		// Token: 0x02001DAF RID: 7599
		private struct NavigationTableInfo
		{
			// Token: 0x0600BC95 RID: 48277 RVA: 0x0026432F File Offset: 0x0026252F
			public NavigationTableInfo(string dataColumnName, string itemKindColumnName, string previewDelayColumnName, string isLeafColumnName)
			{
				this.dataColumnName = dataColumnName;
				this.itemKindColumnName = itemKindColumnName;
				this.previewDelayColumnName = previewDelayColumnName;
				this.isLeafColumnName = isLeafColumnName;
			}

			// Token: 0x17002E6B RID: 11883
			// (get) Token: 0x0600BC96 RID: 48278 RVA: 0x0026434E File Offset: 0x0026254E
			public string DataColumnName
			{
				get
				{
					return this.dataColumnName;
				}
			}

			// Token: 0x17002E6C RID: 11884
			// (get) Token: 0x0600BC97 RID: 48279 RVA: 0x00264356 File Offset: 0x00262556
			public string ItemKindColumnName
			{
				get
				{
					return this.itemKindColumnName;
				}
			}

			// Token: 0x17002E6D RID: 11885
			// (get) Token: 0x0600BC98 RID: 48280 RVA: 0x0026435E File Offset: 0x0026255E
			public string PreviewDelayColumnName
			{
				get
				{
					return this.previewDelayColumnName;
				}
			}

			// Token: 0x17002E6E RID: 11886
			// (get) Token: 0x0600BC99 RID: 48281 RVA: 0x00264366 File Offset: 0x00262566
			public string IsLeafColumnName
			{
				get
				{
					return this.isLeafColumnName;
				}
			}

			// Token: 0x0400600A RID: 24586
			private readonly string dataColumnName;

			// Token: 0x0400600B RID: 24587
			private readonly string itemKindColumnName;

			// Token: 0x0400600C RID: 24588
			private readonly string previewDelayColumnName;

			// Token: 0x0400600D RID: 24589
			private readonly string isLeafColumnName;
		}

		// Token: 0x02001DB0 RID: 7600
		private struct Mark
		{
			// Token: 0x0600BC9A RID: 48282 RVA: 0x0026436E File Offset: 0x0026256E
			public Mark(ValueSerializer.StringInterner interner, StringBuilder builder)
			{
				this.interner = interner;
				this.builder = builder;
				this.length = this.builder.Length;
			}

			// Token: 0x17002E6F RID: 11887
			// (get) Token: 0x0600BC9B RID: 48283 RVA: 0x0026438F File Offset: 0x0026258F
			public int Index
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x0600BC9C RID: 48284 RVA: 0x00264397 File Offset: 0x00262597
			public void ResetTo()
			{
				this.interner.AdjustForResetTo(this.length);
				this.builder.Length = this.length;
			}

			// Token: 0x0400600E RID: 24590
			private readonly ValueSerializer.StringInterner interner;

			// Token: 0x0400600F RID: 24591
			private readonly StringBuilder builder;

			// Token: 0x04006010 RID: 24592
			private readonly int length;
		}

		// Token: 0x02001DB1 RID: 7601
		private struct ValueScope : IDisposable
		{
			// Token: 0x0600BC9D RID: 48285 RVA: 0x002643BB File Offset: 0x002625BB
			public ValueScope(ValueSerializer serializer)
			{
				this.serializer = serializer;
				this.lastTruncated = serializer.truncated;
				serializer.valueDepth++;
			}

			// Token: 0x17002E70 RID: 11888
			// (get) Token: 0x0600BC9E RID: 48286 RVA: 0x002643DE File Offset: 0x002625DE
			public bool IsTruncated
			{
				get
				{
					return this.serializer.truncated;
				}
			}

			// Token: 0x17002E71 RID: 11889
			// (get) Token: 0x0600BC9F RID: 48287 RVA: 0x002643EB File Offset: 0x002625EB
			public bool ValueDepthExceeded
			{
				get
				{
					return this.serializer.valueDepth > this.serializer.options.MaxValueDepth;
				}
			}

			// Token: 0x0600BCA0 RID: 48288 RVA: 0x0026440A File Offset: 0x0026260A
			public void Dispose()
			{
				this.serializer.truncated = this.lastTruncated;
				this.serializer.valueDepth--;
				this.serializer = null;
			}

			// Token: 0x04006011 RID: 24593
			private ValueSerializer serializer;

			// Token: 0x04006012 RID: 24594
			private bool lastTruncated;
		}

		// Token: 0x02001DB2 RID: 7602
		private struct DelayedField : IEquatable<ValueSerializer.DelayedField>
		{
			// Token: 0x0600BCA1 RID: 48289 RVA: 0x00264437 File Offset: 0x00262637
			public DelayedField(string itemKind, string typeName, bool? isLeaf)
			{
				this.itemKind = itemKind;
				this.typeName = typeName;
				this.isLeaf = isLeaf;
			}

			// Token: 0x0600BCA2 RID: 48290 RVA: 0x00264450 File Offset: 0x00262650
			bool IEquatable<ValueSerializer.DelayedField>.Equals(ValueSerializer.DelayedField other)
			{
				return string.Equals(this.itemKind, other.itemKind, StringComparison.Ordinal) && string.Equals(this.typeName, other.typeName, StringComparison.Ordinal) && this.isLeaf.Equals(other.isLeaf);
			}

			// Token: 0x0600BCA3 RID: 48291 RVA: 0x002644A4 File Offset: 0x002626A4
			public override int GetHashCode()
			{
				int num = 0;
				if (this.itemKind != null)
				{
					num ^= this.itemKind.GetHashCode();
				}
				if (this.typeName != null)
				{
					num ^= this.typeName.GetHashCode();
				}
				if (this.isLeaf != null)
				{
					num ^= this.isLeaf.Value.GetHashCode();
				}
				return num;
			}

			// Token: 0x0600BCA4 RID: 48292 RVA: 0x00264504 File Offset: 0x00262704
			public IValue CreateValue(IEngine engine)
			{
				ArrayBuilder<string> arrayBuilder = new ArrayBuilder<string>(3);
				ArrayBuilder<IValue> arrayBuilder2 = new ArrayBuilder<IValue>(3);
				arrayBuilder.Add("Preview.Delay");
				IValue value2;
				if (this.typeName != null)
				{
					IValue value = engine.Text(this.typeName);
					value2 = value;
				}
				else
				{
					value2 = engine.Null;
				}
				arrayBuilder2.Add(value2);
				if (this.itemKind != null)
				{
					arrayBuilder.Add("NavigationTable.ItemKind");
					arrayBuilder2.Add(engine.Text(this.itemKind));
				}
				if (this.isLeaf != null)
				{
					arrayBuilder.Add("NavigationTable.IsLeaf");
					arrayBuilder2.Add(engine.Text(engine.Logical(this.isLeaf.Value).ToSource()));
				}
				IRecordValue recordValue = engine.Record(engine.Keys(arrayBuilder.ToArray()), arrayBuilder2.ToArray());
				return engine.Null.NewMeta(recordValue);
			}

			// Token: 0x04006013 RID: 24595
			private string itemKind;

			// Token: 0x04006014 RID: 24596
			private string typeName;

			// Token: 0x04006015 RID: 24597
			private bool? isLeaf;
		}

		// Token: 0x02001DB3 RID: 7603
		private enum AllowCustomTypes
		{
			// Token: 0x04006017 RID: 24599
			No,
			// Token: 0x04006018 RID: 24600
			Truncate,
			// Token: 0x04006019 RID: 24601
			Yes
		}

		// Token: 0x02001DB4 RID: 7604
		private class StringInterner
		{
			// Token: 0x0600BCA5 RID: 48293 RVA: 0x002645DC File Offset: 0x002627DC
			public StringInterner(IEngine engine)
			{
				this.engine = engine;
				this.found = new Dictionary<string, ValueSerializer.StringInterner.Reference>();
				this.replacement = 0;
			}

			// Token: 0x0600BCA6 RID: 48294 RVA: 0x00264600 File Offset: 0x00262800
			public void WriteText(string text, StringBuilder builder)
			{
				string text2 = this.engine.EscapeString(text);
				if (text2.Length < 20)
				{
					builder.Append(text2);
					return;
				}
				ValueSerializer.StringInterner.Reference reference;
				if (!this.found.TryGetValue(text2, out reference))
				{
					this.found[text2] = new ValueSerializer.StringInterner.Reference(builder.Length);
					builder.Append(text2);
					return;
				}
				string text3 = reference.AddReference(this, builder, text2);
				builder.Append(text3);
			}

			// Token: 0x0600BCA7 RID: 48295 RVA: 0x00264670 File Offset: 0x00262870
			public void AdjustForResetTo(int index)
			{
				foreach (KeyValuePair<string, ValueSerializer.StringInterner.Reference> keyValuePair in this.found)
				{
					keyValuePair.Value.AdjustForResetTo(index);
				}
			}

			// Token: 0x0600BCA8 RID: 48296 RVA: 0x002646CC File Offset: 0x002628CC
			public void ApplyEdits(StringBuilder builder)
			{
				foreach (KeyValuePair<string, ValueSerializer.StringInterner.Reference> keyValuePair in this.found.OrderByDescending((KeyValuePair<string, ValueSerializer.StringInterner.Reference> pair) => pair.Value.Position))
				{
					keyValuePair.Value.ApplyEdit(builder, keyValuePair.Key);
				}
			}

			// Token: 0x0600BCA9 RID: 48297 RVA: 0x0026474C File Offset: 0x0026294C
			public IEnumerable<string> GetWriteValues()
			{
				foreach (KeyValuePair<string, ValueSerializer.StringInterner.Reference> keyValuePair in this.found)
				{
					string symbol = keyValuePair.Value.Symbol;
					if (symbol != null)
					{
						yield return symbol + "=" + keyValuePair.Key;
					}
				}
				Dictionary<string, ValueSerializer.StringInterner.Reference>.Enumerator enumerator = default(Dictionary<string, ValueSerializer.StringInterner.Reference>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x0600BCAA RID: 48298 RVA: 0x0026475C File Offset: 0x0026295C
			private static string FormatReference(string reference)
			{
				return string.Format(CultureInfo.InvariantCulture, "@{0}[{1}]", "_v", reference);
			}

			// Token: 0x0400601A RID: 24602
			private const int minLength = 20;

			// Token: 0x0400601B RID: 24603
			private readonly IEngine engine;

			// Token: 0x0400601C RID: 24604
			private readonly Dictionary<string, ValueSerializer.StringInterner.Reference> found;

			// Token: 0x0400601D RID: 24605
			private int replacement;

			// Token: 0x02001DB5 RID: 7605
			private class Reference
			{
				// Token: 0x0600BCAB RID: 48299 RVA: 0x00264773 File Offset: 0x00262973
				public Reference(int position)
				{
					this.position = position;
				}

				// Token: 0x17002E72 RID: 11890
				// (get) Token: 0x0600BCAC RID: 48300 RVA: 0x00264782 File Offset: 0x00262982
				public int Position
				{
					get
					{
						return this.position;
					}
				}

				// Token: 0x17002E73 RID: 11891
				// (get) Token: 0x0600BCAD RID: 48301 RVA: 0x0026478A File Offset: 0x0026298A
				public string Symbol
				{
					get
					{
						return this.symbol;
					}
				}

				// Token: 0x17002E74 RID: 11892
				// (get) Token: 0x0600BCAE RID: 48302 RVA: 0x00264792 File Offset: 0x00262992
				public string SymbolReference
				{
					get
					{
						return ValueSerializer.StringInterner.FormatReference(this.symbol);
					}
				}

				// Token: 0x17002E75 RID: 11893
				// (get) Token: 0x0600BCAF RID: 48303 RVA: 0x0026479F File Offset: 0x0026299F
				public bool IsFirst
				{
					get
					{
						return this.symbol == null;
					}
				}

				// Token: 0x17002E76 RID: 11894
				// (get) Token: 0x0600BCB0 RID: 48304 RVA: 0x002647AA File Offset: 0x002629AA
				public bool IsStale
				{
					get
					{
						return this.position == -1;
					}
				}

				// Token: 0x0600BCB1 RID: 48305 RVA: 0x002647B8 File Offset: 0x002629B8
				public string AddReference(ValueSerializer.StringInterner interner, StringBuilder builder, string escaped)
				{
					if (this.IsStale && this.IsFirst)
					{
						this.position = builder.Length;
						return escaped;
					}
					if (this.IsFirst)
					{
						IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
						string text = "s{0}";
						int replacement = interner.replacement;
						interner.replacement = replacement + 1;
						this.symbol = string.Format(invariantCulture, text, replacement);
					}
					return this.SymbolReference;
				}

				// Token: 0x0600BCB2 RID: 48306 RVA: 0x0026481C File Offset: 0x00262A1C
				public void AdjustForResetTo(int index)
				{
					if (index < this.position)
					{
						this.position = -1;
					}
				}

				// Token: 0x0600BCB3 RID: 48307 RVA: 0x0026482E File Offset: 0x00262A2E
				public void ApplyEdit(StringBuilder builder, string escaped)
				{
					if (!this.IsStale && !this.IsFirst)
					{
						builder.Replace(escaped, this.SymbolReference, this.position, escaped.Length);
						this.position = -1;
					}
				}

				// Token: 0x0400601E RID: 24606
				private const int Stale = -1;

				// Token: 0x0400601F RID: 24607
				private int position;

				// Token: 0x04006020 RID: 24608
				private string symbol;
			}
		}
	}
}
