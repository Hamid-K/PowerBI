using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001D RID: 29
	internal sealed class BasXmlFunctionReader : BasXmlReader
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005E47 File Offset: 0x00004047
		public BasXmlFunctionReader(IRfcFunction function, Action<string> logger = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			this.logger = logger;
			this.function = function;
			this.recordStack = new Stack<IRfcDataContainer>();
			this.currentRecord = null;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005E7D File Offset: 0x0000407D
		protected override string TagOfInterest
		{
			get
			{
				return "values";
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005E84 File Offset: 0x00004084
		public void FunctionRead(byte[] xmlBytes)
		{
			if (xmlBytes == null || xmlBytes.Length == 0)
			{
				return;
			}
			using (this.stream = new MemoryStream(xmlBytes, false))
			{
				try
				{
					base.ParseHeader();
					goto IL_002A;
				}
				catch (SapBwException)
				{
					goto IL_002A;
				}
				IL_0024:
				this.ProcessEndTag();
				IL_002A:
				if (base.ReadNext())
				{
					goto IL_0024;
				}
			}
			this.stream = null;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005EF4 File Offset: 0x000040F4
		protected override void StartTag(int identifierIndex)
		{
			this.depth++;
			if (this.itemDepth == null && this.fieldName == this.TagOfInterest)
			{
				this.itemDepth = new int?(this.depth + 1);
			}
			if (this.itemDepth == null || this.depth != this.itemDepth.Value)
			{
				if (this.currentRecord != null)
				{
					if (this.currentRecord.ContainerType == 1 && this.createNewRow)
					{
						IRfcTable rfcTable = (IRfcTable)this.currentRecord;
						this.createNewRow = false;
						rfcTable.Append();
						return;
					}
					try
					{
						switch (this.currentRecord.ContainerType)
						{
						case 0:
							this.fieldIndex = ((IRfcStructure)this.currentRecord).Metadata.TryNameToIndex(this.fieldName);
							break;
						case 1:
							this.fieldIndex = ((IRfcTable)this.currentRecord).Metadata.LineType.TryNameToIndex(this.fieldName);
							break;
						case 2:
							this.fieldIndex = ((IRfcAbapObject)this.currentRecord).Metadata.TryNameToIndex(this.fieldName);
							break;
						case 3:
							this.fieldIndex = ((IRfcFunction)this.currentRecord).Metadata.TryNameToIndex(this.fieldName);
							break;
						default:
							this.fieldIndex = -1;
							break;
						}
					}
					catch (InvalidCastException)
					{
						this.fieldIndex = -1;
					}
				}
				return;
			}
			if (this.function.Metadata.TryNameToIndex(this.fieldName) == -1)
			{
				this.Log(string.Concat(new string[]
				{
					"WARNING: FieldName not found ",
					this.fieldName,
					" for function ",
					this.function.Metadata.Name,
					"."
				}));
				return;
			}
			IRfcParameter rfcParameter = this.function[this.fieldName];
			rfcParameter.Active = true;
			if (rfcParameter.Metadata.DataType < 24)
			{
				this.fieldIndex = this.function.Metadata.NameToIndex(this.fieldName);
				this.recordStack.Push(this.currentRecord = this.function);
				return;
			}
			switch (rfcParameter.Metadata.DataType)
			{
			case 24:
				this.currentRecord = rfcParameter.GetStructure();
				break;
			case 25:
			{
				IRfcTable table = rfcParameter.GetTable();
				table.Clear();
				this.createNewRow = true;
				this.currentRecord = table;
				break;
			}
			case 26:
				this.currentRecord = rfcParameter.GetAbapObject();
				break;
			default:
				throw new SapBwException(Resources.UnsupportedParameterType(rfcParameter.Metadata.DataType));
			}
			this.recordStack.Push(this.currentRecord);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000061CC File Offset: 0x000043CC
		private void ProcessEndTag()
		{
			if (this.itemDepth != null)
			{
				if (this.currentRecord != null && this.currentRecord.ContainerType == 1)
				{
					if (this.depth == this.itemDepth.Value - 1)
					{
						this.createNewRow = false;
						this.CloseRfcTable();
						return;
					}
					if (this.depth == this.itemDepth.Value)
					{
						this.createNewRow = true;
						return;
					}
				}
				else if (this.depth == this.itemDepth.Value - 1)
				{
					if (this.recordStack.Count > 0)
					{
						this.recordStack.Pop();
					}
					else
					{
						this.Log("WARNING: Expected item in stack but found none while closing tag: " + this.fieldName);
					}
					this.currentRecord = ((this.recordStack.Count > 0) ? this.recordStack.Peek() : null);
					this.fieldName = null;
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000062B0 File Offset: 0x000044B0
		private void CloseRfcTable()
		{
			IRfcTable rfcTable = (IRfcTable)this.currentRecord;
			if (rfcTable.RowCount > 0)
			{
				rfcTable.CurrentIndex = 0;
			}
			this.recordStack.Pop();
			this.currentRecord = ((this.recordStack.Count > 0) ? this.recordStack.Peek() : null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006308 File Offset: 0x00004508
		protected override void SaveValue(int length)
		{
			if (this.fieldName == null)
			{
				throw new Exception(Resources.NullFieldName);
			}
			string text = base.ConvertToString(length);
			if (this.fieldIndex == -1)
			{
				this.Log(string.Concat(new string[] { "WARNING: Invalid fieldIndex while saving ", this.fieldName, " : '", text, "'" }));
				return;
			}
			RfcElementMetadata elementMetadata = this.currentRecord.GetElementMetadata(this.fieldIndex);
			RfcDataType rfcDataType;
			if (elementMetadata.NucLength < text.Length)
			{
				rfcDataType = elementMetadata.DataType;
				if (rfcDataType != null)
				{
					if (rfcDataType == 2)
					{
						text = text.Substring(text.Length - elementMetadata.NucLength);
					}
				}
				else
				{
					text = text.Substring(0, elementMetadata.NucLength);
				}
			}
			rfcDataType = elementMetadata.DataType;
			if (rfcDataType <= 8)
			{
				if (rfcDataType == 6)
				{
					this.currentRecord.SetValue(this.fieldIndex, text.Substring(0, text.Length - 1).Replace('.', ','));
					return;
				}
				if (rfcDataType - 7 <= 1)
				{
					this.currentRecord.SetValue(this.fieldIndex, text.Substring(0, text.Length - 1));
					return;
				}
			}
			else
			{
				if (rfcDataType == 14)
				{
					this.currentRecord.SetValue(this.fieldIndex, text.Substring(2));
					return;
				}
				if (rfcDataType == 15)
				{
					this.currentRecord.SetValue(this.fieldIndex, double.Parse(text, CultureInfo.InvariantCulture.NumberFormat));
					return;
				}
			}
			this.currentRecord.SetValue(this.fieldIndex, text);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006488 File Offset: 0x00004688
		private void Log(string message)
		{
			if (this.logger != null)
			{
				this.logger(message);
			}
		}

		// Token: 0x0400007A RID: 122
		private const string Values = "values";

		// Token: 0x0400007B RID: 123
		private readonly IRfcFunction function;

		// Token: 0x0400007C RID: 124
		private readonly Stack<IRfcDataContainer> recordStack;

		// Token: 0x0400007D RID: 125
		private readonly Action<string> logger;

		// Token: 0x0400007E RID: 126
		private bool createNewRow;

		// Token: 0x0400007F RID: 127
		private IRfcDataContainer currentRecord;
	}
}
