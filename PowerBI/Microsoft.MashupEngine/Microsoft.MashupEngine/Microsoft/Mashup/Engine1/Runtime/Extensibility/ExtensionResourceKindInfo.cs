using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200170A RID: 5898
	internal class ExtensionResourceKindInfo
	{
		// Token: 0x060095E7 RID: 38375 RVA: 0x001F0D7C File Offset: 0x001EEF7C
		public ExtensionResourceKindInfo(object moduleLock, string resourceKind, FunctionValue makeResourcePath, FunctionValue parseResourcePath, FunctionValue nativeQuery, FunctionValue testConnection, FunctionValue getHostName, List<ExtensionDataSourceLocationFactory> dsrProtocolHandlersList, RecordValue resourceRecord)
		{
			this.moduleLock = moduleLock;
			this.resourceKind = resourceKind;
			this.makeResourcePath = makeResourcePath;
			this.makeResourcePathParameters = makeResourcePath.Type.AsFunctionType.Parameters;
			this.parseResourcePath = parseResourcePath;
			this.nativeQuery = nativeQuery;
			this.testConnection = testConnection;
			this.getHostName = getHostName;
			this.dsrProtocolHandlersList = dsrProtocolHandlersList;
			this.resourceRecord = resourceRecord;
		}

		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x060095E8 RID: 38376 RVA: 0x001F0DEA File Offset: 0x001EEFEA
		public Keys MakeResourcePathParameters
		{
			get
			{
				return this.makeResourcePathParameters.Keys;
			}
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x060095E9 RID: 38377 RVA: 0x001F0DF7 File Offset: 0x001EEFF7
		public bool HasNativeQuery
		{
			get
			{
				return this.nativeQuery != null;
			}
		}

		// Token: 0x17002731 RID: 10033
		// (get) Token: 0x060095EA RID: 38378 RVA: 0x001F0E02 File Offset: 0x001EF002
		public Keys NativeQueryParameters
		{
			get
			{
				return this.nativeQuery.Type.AsFunctionType.Parameters.Keys;
			}
		}

		// Token: 0x17002732 RID: 10034
		// (get) Token: 0x060095EB RID: 38379 RVA: 0x001F0E1E File Offset: 0x001EF01E
		public RecordValue ResourceRecord
		{
			get
			{
				return this.resourceRecord;
			}
		}

		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x060095EC RID: 38380 RVA: 0x001F0E26 File Offset: 0x001EF026
		public IList<ExtensionDataSourceLocationFactory> DsrHandlers
		{
			get
			{
				return this.dsrProtocolHandlersList;
			}
		}

		// Token: 0x060095ED RID: 38381 RVA: 0x001F0E30 File Offset: 0x001EF030
		public Value MakeResourcePath(Value[] args)
		{
			object obj = this.moduleLock;
			Value value;
			lock (obj)
			{
				value = this.makeResourcePath.Invoke(args);
			}
			return value;
		}

		// Token: 0x060095EE RID: 38382 RVA: 0x001F0E78 File Offset: 0x001EF078
		public Value NativeQuery(Value[] args)
		{
			object obj = this.moduleLock;
			Value value;
			lock (obj)
			{
				value = this.nativeQuery.Invoke(args);
			}
			return value;
		}

		// Token: 0x060095EF RID: 38383 RVA: 0x001F0EC0 File Offset: 0x001EF0C0
		public string CreateTestFormula(string resourcePath)
		{
			if (this.testConnection != null)
			{
				return this.CreateTestConnectionFormula(resourcePath);
			}
			IResource resource = new Resource(this.resourceKind, resourcePath, resourcePath);
			using (IEnumerator<ExtensionDataSourceLocationFactory> enumerator = this.dsrProtocolHandlersList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IDataSourceLocation dataSourceLocation;
					if (enumerator.Current.TryCreateFromResource(resource, false, out dataSourceLocation))
					{
						IFormulaCreationResult formulaCreationResult = dataSourceLocation.CreateFormula(false);
						if (formulaCreationResult.Success)
						{
							return formulaCreationResult.Formula;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060095F0 RID: 38384 RVA: 0x001F0F4C File Offset: 0x001EF14C
		private string CreateTestConnectionFormula(string resourcePath)
		{
			object obj = this.moduleLock;
			string text;
			lock (obj)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (IValueReference valueReference in this.testConnection.Invoke(TextValue.New(resourcePath)).AsList)
				{
					if (num == 0)
					{
						stringBuilder.Append(valueReference.Value.AsText.AsString);
						stringBuilder.Append('(');
					}
					else
					{
						if (num > 1)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(ToSourceVisitor.ToSource(valueReference.Value));
					}
					num++;
				}
				stringBuilder.Append(')');
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060095F1 RID: 38385 RVA: 0x001F103C File Offset: 0x001EF23C
		public IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
		{
			if (this.parseResourcePath == null || this.makeResourcePath == null)
			{
				return null;
			}
			object obj = this.moduleLock;
			IEnumerable<KeyValuePair<string, string>> enumerable;
			lock (obj)
			{
				ListValue asList = this.parseResourcePath.Invoke(TextValue.New(resourcePath)).AsList;
				KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[asList.Count];
				for (int i = 0; i < asList.Count; i++)
				{
					Value value;
					string text;
					if (this.makeResourcePathParameters[i].TryGetMetaField("Documentation.Description", out value) && value.IsText)
					{
						text = value.AsString;
					}
					else
					{
						text = this.makeResourcePathParameters.Keys[i];
					}
					string text2;
					if (asList[i].IsNull)
					{
						text2 = null;
					}
					else if (asList[i].IsNumber)
					{
						text2 = asList[i].ToString();
					}
					else
					{
						text2 = asList[i].AsText.String;
					}
					array[i] = new KeyValuePair<string, string>(text, text2);
				}
				enumerable = array;
			}
			return enumerable;
		}

		// Token: 0x060095F2 RID: 38386 RVA: 0x001F1168 File Offset: 0x001EF368
		public bool TryGetHostName(string resourcePath, out string hostName)
		{
			object obj = this.moduleLock;
			bool flag2;
			lock (obj)
			{
				if (this.getHostName != null)
				{
					hostName = this.getHostName.Invoke(TextValue.New(resourcePath)).AsString;
					flag2 = true;
				}
				else
				{
					hostName = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060095F3 RID: 38387 RVA: 0x001F11CC File Offset: 0x001EF3CC
		public bool TryParseResourcePath(string resourcePath, out IResource resource, out string errorMessage)
		{
			bool flag;
			try
			{
				object obj = this.moduleLock;
				lock (obj)
				{
					ListValue asList = this.parseResourcePath.Invoke(TextValue.New(resourcePath)).AsList;
				}
				resource = new Resource(this.resourceKind, resourcePath, resourcePath);
				errorMessage = null;
				flag = true;
			}
			catch (ValueException ex)
			{
				resource = null;
				errorMessage = ex.Message;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04004FB7 RID: 20407
		private readonly object moduleLock;

		// Token: 0x04004FB8 RID: 20408
		private readonly string resourceKind;

		// Token: 0x04004FB9 RID: 20409
		private readonly FunctionValue makeResourcePath;

		// Token: 0x04004FBA RID: 20410
		private readonly RecordValue makeResourcePathParameters;

		// Token: 0x04004FBB RID: 20411
		private readonly FunctionValue parseResourcePath;

		// Token: 0x04004FBC RID: 20412
		private readonly FunctionValue nativeQuery;

		// Token: 0x04004FBD RID: 20413
		private readonly FunctionValue testConnection;

		// Token: 0x04004FBE RID: 20414
		private readonly FunctionValue getHostName;

		// Token: 0x04004FBF RID: 20415
		private readonly IList<ExtensionDataSourceLocationFactory> dsrProtocolHandlersList;

		// Token: 0x04004FC0 RID: 20416
		private readonly RecordValue resourceRecord;
	}
}
