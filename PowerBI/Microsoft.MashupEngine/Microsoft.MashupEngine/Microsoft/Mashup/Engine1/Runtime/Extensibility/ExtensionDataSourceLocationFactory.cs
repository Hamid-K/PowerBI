using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Shims.Json;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x020016F7 RID: 5879
	internal class ExtensionDataSourceLocationFactory : IDataSourceLocationFactory
	{
		// Token: 0x0600955B RID: 38235 RVA: 0x001EDEFC File Offset: 0x001EC0FC
		public ExtensionDataSourceLocationFactory(object moduleLock, string resourceKind, string protocol, FunctionValue getAddressFunction, FunctionValue getFormulaFunction, FunctionValue getFriendlyNameFunction, FunctionValue normalizeFunction, FunctionValue makeResourcePath, FunctionValue parseResourcePath, RecordValue exports)
		{
			this.moduleLock = moduleLock;
			this.resourceKind = resourceKind;
			this.protocol = protocol;
			this.getAddressFunction = getAddressFunction;
			this.getFormulaFunction = getFormulaFunction;
			this.getFriendlyNameFunction = getFriendlyNameFunction;
			this.normalizeFunction = normalizeFunction;
			this.makeResourcePath = makeResourcePath;
			this.parseResourcePath = parseResourcePath;
			this.exports = new Dictionary<string, FunctionTypeValue>(exports.Count);
			for (int i = 0; i < exports.Keys.Length; i++)
			{
				Value value = exports[i];
				if (value.IsFunction)
				{
					this.exports[exports.Keys[i]] = value.AsFunction.Type.AsFunctionType;
				}
			}
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x0600955C RID: 38236 RVA: 0x001EDFB6 File Offset: 0x001EC1B6
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x0600955D RID: 38237 RVA: 0x001EDFBE File Offset: 0x001EC1BE
		public IDataSourceLocation New()
		{
			return new ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation(this);
		}

		// Token: 0x0600955E RID: 38238 RVA: 0x001EDFC8 File Offset: 0x001EC1C8
		public bool TryCreateFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
		{
			if (resource.Kind == this.resourceKind)
			{
				object obj = this.moduleLock;
				lock (obj)
				{
					try
					{
						ListValue asList = this.parseResourcePath.Invoke(TextValue.New(resource.NonNormalizedPath)).AsList;
						Value value = this.getAddressFunction.Invoke(asList.ToArray());
						RecordValue asRecord = (value.IsList ? value[0] : value).AsRecord;
						location = new ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation(this, asRecord);
						if (normalize)
						{
							location.Normalize();
						}
						return true;
					}
					catch (ValueException)
					{
					}
				}
			}
			location = null;
			return false;
		}

		// Token: 0x0600955F RID: 38239 RVA: 0x001EE08C File Offset: 0x001EC28C
		public bool TryGetLocation(IExpression expression, ListValue navigationSteps, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
		{
			string text;
			Dictionary<string, Value> dictionary;
			if (ExtensionDataSourceLocationFactory.FunctionExtractorVisitor.TryFind(expression, this.exports, out text, out dictionary))
			{
				FunctionTypeValue asFunctionType = this.getAddressFunction.Type.AsFunctionType;
				Value[] array = ExtensionDataSourceLocationFactory.MapArguments(this.exports.Values, asFunctionType, dictionary);
				bool flag = array != null;
				try
				{
					object obj = this.moduleLock;
					lock (obj)
					{
						if (flag && asFunctionType.ParameterCount > array.Length && asFunctionType.Min < asFunctionType.ParameterCount)
						{
							array = array.Add(navigationSteps);
						}
						else if (!navigationSteps.IsEmpty)
						{
							flag = false;
						}
						if (flag)
						{
							Value value = this.getAddressFunction.Invoke(array);
							if (value.IsRecord)
							{
								location = new ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation(this, value.AsRecord);
								foundOptions = RecordValue.Empty;
								unknownOptions = Keys.Empty;
								return true;
							}
							if (value.IsList && value.AsList.Count == 2)
							{
								location = new ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation(this, value.AsList[0].AsRecord);
								foundOptions = value.AsList[1].AsRecord;
								unknownOptions = Keys.Empty;
								return true;
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
			}
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x06009560 RID: 38240 RVA: 0x001EE1FC File Offset: 0x001EC3FC
		private static Value[] MapArguments(FunctionTypeValue functionType, Dictionary<string, Value> arguments)
		{
			Value[] array = new Value[functionType.ParameterCount];
			for (int i = 0; i < array.Length; i++)
			{
				if (!arguments.TryGetValue(functionType.ParameterName(i), out array[i]))
				{
					if (i < functionType.Min)
					{
						return null;
					}
					array[i] = Value.Null;
				}
			}
			return array;
		}

		// Token: 0x06009561 RID: 38241 RVA: 0x001EE250 File Offset: 0x001EC450
		private static Value[] MapArguments(IEnumerable<FunctionTypeValue> exports, FunctionTypeValue functionType, Dictionary<string, Value> arguments)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (FunctionTypeValue functionTypeValue in exports)
			{
				hashSet.UnionWith(functionTypeValue.Parameters.Keys);
			}
			Value[] array = new Value[functionType.ParameterCount];
			for (int i = 0; i < array.Length; i++)
			{
				string text = functionType.ParameterName(i);
				if (!arguments.TryGetValue(text, out array[i]))
				{
					if (i < functionType.Min)
					{
						return null;
					}
					if (hashSet.Contains(text))
					{
						array[i] = Value.Null;
					}
					else
					{
						if (i == array.Length - 1)
						{
							Array.Resize<Value>(ref array, array.Length - 1);
							return array;
						}
						return null;
					}
				}
			}
			return array;
		}

		// Token: 0x04004F65 RID: 20325
		private readonly object moduleLock;

		// Token: 0x04004F66 RID: 20326
		private readonly string resourceKind;

		// Token: 0x04004F67 RID: 20327
		private readonly string protocol;

		// Token: 0x04004F68 RID: 20328
		private readonly FunctionValue getAddressFunction;

		// Token: 0x04004F69 RID: 20329
		private readonly FunctionValue getFormulaFunction;

		// Token: 0x04004F6A RID: 20330
		private readonly FunctionValue getFriendlyNameFunction;

		// Token: 0x04004F6B RID: 20331
		private readonly FunctionValue normalizeFunction;

		// Token: 0x04004F6C RID: 20332
		private readonly FunctionValue makeResourcePath;

		// Token: 0x04004F6D RID: 20333
		private readonly FunctionValue parseResourcePath;

		// Token: 0x04004F6E RID: 20334
		private readonly Dictionary<string, FunctionTypeValue> exports;

		// Token: 0x020016F8 RID: 5880
		private sealed class ExtensionDataSourceLocation : IDataSourceLocation, IEquatable<IDataSourceLocation>, IComparable<IDataSourceLocation>
		{
			// Token: 0x06009562 RID: 38242 RVA: 0x001EE320 File Offset: 0x001EC520
			public ExtensionDataSourceLocation(ExtensionDataSourceLocationFactory factory)
			{
				this.factory = factory;
			}

			// Token: 0x06009563 RID: 38243 RVA: 0x001EE32F File Offset: 0x001EC52F
			public ExtensionDataSourceLocation(ExtensionDataSourceLocationFactory factory, RecordValue dsr)
				: this(factory)
			{
				this.SetDsr(dsr);
			}

			// Token: 0x17002714 RID: 10004
			// (get) Token: 0x06009564 RID: 38244 RVA: 0x001EE33F File Offset: 0x001EC53F
			public string Protocol
			{
				get
				{
					return this.factory.Protocol;
				}
			}

			// Token: 0x17002715 RID: 10005
			// (get) Token: 0x06009565 RID: 38245 RVA: 0x001EE34C File Offset: 0x001EC54C
			// (set) Token: 0x06009566 RID: 38246 RVA: 0x001EE354 File Offset: 0x001EC554
			public IDictionary<string, object> Address
			{
				get
				{
					return this.address;
				}
				set
				{
					this.address = value;
				}
			}

			// Token: 0x17002716 RID: 10006
			// (get) Token: 0x06009567 RID: 38247 RVA: 0x001EE35D File Offset: 0x001EC55D
			// (set) Token: 0x06009568 RID: 38248 RVA: 0x001EE365 File Offset: 0x001EC565
			public string Authentication
			{
				get
				{
					return this.authentication;
				}
				set
				{
					this.authentication = value;
				}
			}

			// Token: 0x17002717 RID: 10007
			// (get) Token: 0x06009569 RID: 38249 RVA: 0x001EE36E File Offset: 0x001EC56E
			// (set) Token: 0x0600956A RID: 38250 RVA: 0x001EE376 File Offset: 0x001EC576
			public string Query
			{
				get
				{
					return this.query;
				}
				set
				{
					this.query = value;
				}
			}

			// Token: 0x17002718 RID: 10008
			// (get) Token: 0x0600956B RID: 38251 RVA: 0x001EE37F File Offset: 0x001EC57F
			public string ResourceKind
			{
				get
				{
					return this.factory.resourceKind;
				}
			}

			// Token: 0x17002719 RID: 10009
			// (get) Token: 0x0600956C RID: 38252 RVA: 0x001EE38C File Offset: 0x001EC58C
			public string FriendlyName
			{
				get
				{
					if (this.factory.getFriendlyNameFunction == null)
					{
						return this.Protocol;
					}
					object moduleLock = this.factory.moduleLock;
					string asString;
					lock (moduleLock)
					{
						asString = this.factory.getFriendlyNameFunction.Invoke(this.GetDsr()).AsString;
					}
					return asString;
				}
			}

			// Token: 0x1700271A RID: 10010
			// (get) Token: 0x0600956D RID: 38253 RVA: 0x001EE3FC File Offset: 0x001EC5FC
			public IEnumerable<string> DisplayAddressFields
			{
				get
				{
					IResource resource;
					ResourceKindInfo resourceKindInfo;
					if (this.TryGetResource(out resource) && ResourceKinds.Lookup(resource.Kind, out resourceKindInfo))
					{
						IEnumerable<KeyValuePair<string, string>> partLabels = resourceKindInfo.GetPartLabels(resource.Path);
						if (partLabels != null)
						{
							return partLabels.Select((KeyValuePair<string, string> kvp) => kvp.Key);
						}
					}
					throw new NotSupportedException(Strings.Resource_Invalid);
				}
			}

			// Token: 0x0600956E RID: 38254 RVA: 0x001EE467 File Offset: 0x001EC667
			public string ToJson()
			{
				if (this.address == null)
				{
					return "null";
				}
				return Library.Text.FromBinary.Invoke(JsonModule.Json.FromValue.Invoke(this.GetDsr())).AsString;
			}

			// Token: 0x0600956F RID: 38255 RVA: 0x001EE496 File Offset: 0x001EC696
			public string ToJson(DataSourceLocationFormat dataSourceLocationFormat)
			{
				if (this.address == null)
				{
					return "null";
				}
				if (dataSourceLocationFormat != DataSourceLocationFormat.Default && dataSourceLocationFormat == DataSourceLocationFormat.SortAddressFields)
				{
					return Json.SerializeObject(this.GetSortedAddressDsr());
				}
				return this.ToJson();
			}

			// Token: 0x06009570 RID: 38256 RVA: 0x001EE4C0 File Offset: 0x001EC6C0
			public IFormulaCreationResult CreateFormula(string optionsJson)
			{
				object moduleLock = this.factory.moduleLock;
				IFormulaCreationResult formulaCreationResult;
				lock (moduleLock)
				{
					string text = null;
					try
					{
						IExpression expression = this.MakeCode(optionsJson);
						if (expression != null)
						{
							return new FormulaCreationResult(expression);
						}
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
						ValueException ex2 = ex as ValueException;
						if (ex2 != null && ex2.MessageString != null)
						{
							text = ex2.MessageString;
						}
						else
						{
							text = ex.Message;
						}
					}
					formulaCreationResult = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.RequestFailure, text);
				}
				return formulaCreationResult;
			}

			// Token: 0x06009571 RID: 38257 RVA: 0x001EE568 File Offset: 0x001EC768
			public IFormulaCreationResult CreateFormula(bool validateAuthentication = false)
			{
				return this.CreateFormula(null);
			}

			// Token: 0x06009572 RID: 38258 RVA: 0x001EE571 File Offset: 0x001EC771
			public string GetAddressFieldLabel(string addressFieldName)
			{
				return DataSourceLocationOperations.GetAddressFieldLabel(addressFieldName);
			}

			// Token: 0x06009573 RID: 38259 RVA: 0x001EE579 File Offset: 0x001EC779
			public bool TryResolve(out IDataSourceLocation resolvedLocation)
			{
				resolvedLocation = new ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation(this.factory, this.GetDsr());
				return true;
			}

			// Token: 0x06009574 RID: 38260 RVA: 0x001EE590 File Offset: 0x001EC790
			public bool TryGetResource(out IResource resource)
			{
				resource = null;
				object moduleLock = this.factory.moduleLock;
				lock (moduleLock)
				{
					try
					{
						IExpression expression = this.MakeCode(null);
						string text;
						Dictionary<string, Value> dictionary;
						if (expression == null || !ExtensionDataSourceLocationFactory.FunctionExtractorVisitor.TryFind(expression, this.factory.exports, out text, out dictionary))
						{
							return false;
						}
						Value[] array = ExtensionDataSourceLocationFactory.MapArguments(this.factory.makeResourcePath.Type.AsFunctionType, dictionary);
						if (array == null)
						{
							return false;
						}
						resource = Resource.New(this.factory.resourceKind, this.factory.makeResourcePath.Invoke(array).AsText.String);
						return true;
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
					}
				}
				return false;
			}

			// Token: 0x06009575 RID: 38261 RVA: 0x001EE66C File Offset: 0x001EC86C
			public void Normalize()
			{
				if (this.factory.normalizeFunction == null)
				{
					return;
				}
				object moduleLock = this.factory.moduleLock;
				lock (moduleLock)
				{
					this.SetDsr(this.factory.normalizeFunction.Invoke(this.GetDsr()).AsRecord);
				}
			}

			// Token: 0x06009576 RID: 38262 RVA: 0x001EE6DC File Offset: 0x001EC8DC
			public override bool Equals(object obj)
			{
				return this.Equals(obj as IDataSourceLocation);
			}

			// Token: 0x06009577 RID: 38263 RVA: 0x001EE6EA File Offset: 0x001EC8EA
			public override int GetHashCode()
			{
				return this.ComputeHashCode();
			}

			// Token: 0x06009578 RID: 38264 RVA: 0x001EE6F2 File Offset: 0x001EC8F2
			public bool Equals(IDataSourceLocation other)
			{
				return this.AreEqual(other);
			}

			// Token: 0x06009579 RID: 38265 RVA: 0x001EE6FB File Offset: 0x001EC8FB
			public int CompareTo(IDataSourceLocation other)
			{
				return string.Compare(this.ComparableRepresentation(), other.ComparableRepresentation(), StringComparison.Ordinal);
			}

			// Token: 0x0600957A RID: 38266 RVA: 0x001EE710 File Offset: 0x001EC910
			private RecordValue GetDsr()
			{
				RecordValue recordValue = ((this.address == null) ? RecordValue.Empty : ValueMarshaller.MarshalFromClrDictionary(this.address, 4).AsRecord);
				RecordValue recordValue2 = RecordValue.New(ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation.dsrKeys, new Value[]
				{
					TextValue.New(this.Protocol),
					recordValue
				});
				if (!string.IsNullOrEmpty(this.query))
				{
					recordValue2 = recordValue2.Concatenate(RecordValue.New(Keys.New("query"), new Value[] { TextValue.New(this.query) })).AsRecord;
				}
				return recordValue2;
			}

			// Token: 0x0600957B RID: 38267 RVA: 0x001EE7A0 File Offset: 0x001EC9A0
			private OrderedDictionary GetSortedAddressDsr()
			{
				SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>();
				if (this.address != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in this.address)
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				OrderedDictionary orderedDictionary = new OrderedDictionary
				{
					{
						ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation.dsrKeys[0],
						this.Protocol
					},
					{
						ExtensionDataSourceLocationFactory.ExtensionDataSourceLocation.dsrKeys[1],
						sortedDictionary
					}
				};
				if (!string.IsNullOrEmpty(this.query))
				{
					orderedDictionary.Add("query", this.query);
				}
				return orderedDictionary;
			}

			// Token: 0x0600957C RID: 38268 RVA: 0x001EE858 File Offset: 0x001ECA58
			private IExpression MakeCode(string optionsJson)
			{
				FunctionValue asFunction = this.factory.getFormulaFunction.Invoke(this.GetDsr(), string.IsNullOrEmpty(optionsJson) ? Value.Null : JsonModule.Json.Document.Invoke(TextValue.New(optionsJson))).AsFunction;
				IFunctionExpression functionExpression = ((IFunctionValue)asFunction).FunctionExpression;
				if (asFunction.Type.AsFunctionType.ParameterCount == 0 && functionExpression != null && functionExpression.Expression != null)
				{
					IExpression expression = ExtensionDataSourceLocationFactory.FunctionIdentifierFixupVisitor.Fixup(functionExpression.Expression);
					expression = FieldAccessInliner.Inline(Engines.Version1, expression);
					return NormalizationVisitor.Normalize(expression, true);
				}
				return null;
			}

			// Token: 0x0600957D RID: 38269 RVA: 0x001EE8E8 File Offset: 0x001ECAE8
			private void SetDsr(RecordValue dsr)
			{
				Value value;
				if (dsr.TryGetValue("protocol", out value) && value.AsText.String != this.Protocol)
				{
					string text = ((value != null && value.IsText) ? value.AsText.String : null);
					throw ValueException.NewDataFormatError<Message2>(Strings.UnexpectedProtocol(this.Protocol, text), value, null);
				}
				if (dsr.TryGetValue("authentication", out value))
				{
					this.authentication = (value.IsNull ? null : value.AsText.String);
				}
				if (dsr.TryGetValue("query", out value))
				{
					this.query = (value.IsNull ? null : value.AsText.String);
				}
				if (dsr.TryGetValue("address", out value))
				{
					this.address = ValueMarshaller.MarshalToClrDictionary(value.AsRecord);
					return;
				}
				this.address = new Dictionary<string, object>();
			}

			// Token: 0x04004F6F RID: 20335
			private static readonly Keys dsrKeys = Keys.New("protocol", "address");

			// Token: 0x04004F70 RID: 20336
			private readonly ExtensionDataSourceLocationFactory factory;

			// Token: 0x04004F71 RID: 20337
			private IDictionary<string, object> address;

			// Token: 0x04004F72 RID: 20338
			private string authentication;

			// Token: 0x04004F73 RID: 20339
			private string query;
		}

		// Token: 0x020016FA RID: 5882
		private sealed class FunctionIdentifierFixupVisitor : AstVisitor2
		{
			// Token: 0x06009582 RID: 38274 RVA: 0x001EE9EE File Offset: 0x001ECBEE
			public static IExpression Fixup(IExpression expression)
			{
				return new ExtensionDataSourceLocationFactory.FunctionIdentifierFixupVisitor().VisitExpression(expression);
			}

			// Token: 0x06009583 RID: 38275 RVA: 0x001EE9FC File Offset: 0x001ECBFC
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				Identifier identifier = null;
				ExpressionKind kind = invocation.Function.Kind;
				Value value;
				if (kind != ExpressionKind.Constant)
				{
					if (kind != ExpressionKind.FieldAccess)
					{
						if (kind == ExpressionKind.Identifier)
						{
							return base.VisitInvocation(invocation);
						}
					}
					else
					{
						identifier = ((IFieldAccessExpression)invocation.Function).MemberName;
					}
				}
				else if (invocation.Function.TryGetConstant(out value) && value.IsFunction && value.Expression.Kind == ExpressionKind.Identifier)
				{
					identifier = ((IIdentifierExpression)value.Expression).Name;
				}
				if (identifier == null)
				{
					throw new NotSupportedException();
				}
				return new InvocationExpressionSyntaxNodeN(new ExclusiveIdentifierExpressionSyntaxNode(identifier), invocation.Arguments.ToArray<IExpression>());
			}
		}

		// Token: 0x020016FB RID: 5883
		private sealed class FunctionExtractorVisitor : AstVisitor2
		{
			// Token: 0x06009585 RID: 38277 RVA: 0x001EEAA5 File Offset: 0x001ECCA5
			private FunctionExtractorVisitor(Dictionary<string, FunctionTypeValue> exports)
			{
				this.exports = exports;
			}

			// Token: 0x06009586 RID: 38278 RVA: 0x001EEAB4 File Offset: 0x001ECCB4
			public static bool TryFind(IExpression code, Dictionary<string, FunctionTypeValue> exports, out string exportName, out Dictionary<string, Value> arguments)
			{
				ExtensionDataSourceLocationFactory.FunctionExtractorVisitor functionExtractorVisitor = new ExtensionDataSourceLocationFactory.FunctionExtractorVisitor(exports);
				functionExtractorVisitor.VisitExpression(code);
				exportName = functionExtractorVisitor.exportName;
				arguments = functionExtractorVisitor.arguments;
				return arguments != null;
			}

			// Token: 0x06009587 RID: 38279 RVA: 0x001EEAE8 File Offset: 0x001ECCE8
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				Identifier functionIdentifier = ExtensionDataSourceLocationFactory.FunctionExtractorVisitor.GetFunctionIdentifier(invocation.Function);
				FunctionTypeValue functionTypeValue;
				if (functionIdentifier != null && this.exports.TryGetValue(functionIdentifier.Name, out functionTypeValue))
				{
					this.exportName = functionIdentifier.Name;
					this.arguments = new Dictionary<string, Value>(functionTypeValue.ParameterCount);
					for (int i = 0; i < functionTypeValue.ParameterCount; i++)
					{
						Value value;
						if (i >= invocation.Arguments.Count)
						{
							value = Value.Null;
						}
						else
						{
							value = ExpressionAnalysis.GetValue(invocation.Arguments[i]);
						}
						if (!ExpressionAnalysis.IsPlaceholder(value))
						{
							this.arguments[functionTypeValue.ParameterName(i)] = value;
						}
					}
				}
				return base.VisitInvocation(invocation);
			}

			// Token: 0x06009588 RID: 38280 RVA: 0x001EEB9C File Offset: 0x001ECD9C
			private static Identifier GetFunctionIdentifier(IExpression expression)
			{
				ExpressionKind kind = expression.Kind;
				Value value;
				if (kind != ExpressionKind.Constant)
				{
					if (kind == ExpressionKind.Identifier)
					{
						return ((IIdentifierExpression)expression).Name;
					}
				}
				else if (expression.TryGetConstant(out value) && value.IsFunction && value.Expression.Kind == ExpressionKind.Identifier)
				{
					return ((IIdentifierExpression)value.Expression).Name;
				}
				return null;
			}

			// Token: 0x04004F76 RID: 20342
			private readonly Dictionary<string, FunctionTypeValue> exports;

			// Token: 0x04004F77 RID: 20343
			private Dictionary<string, Value> arguments;

			// Token: 0x04004F78 RID: 20344
			private string exportName;
		}
	}
}
