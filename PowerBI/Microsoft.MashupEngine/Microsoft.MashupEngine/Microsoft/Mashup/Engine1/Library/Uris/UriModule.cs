using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Uris
{
	// Token: 0x020002B4 RID: 692
	internal sealed class UriModule : Module
	{
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x00039970 File Offset: 0x00037B70
		public override string Name
		{
			get
			{
				return "Uri";
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00039977 File Offset: 0x00037B77
		public override Keys ExportKeys
		{
			get
			{
				if (UriModule.exportKeys == null)
				{
					UriModule.exportKeys = Keys.New(4, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Uri.Combine";
						case 1:
							return "Uri.Parts";
						case 2:
							return "Uri.BuildQueryString";
						case 3:
							return "Uri.EscapeDataString";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return UriModule.exportKeys;
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000399AF File Offset: 0x00037BAF
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return UriModule.Combine;
				case 1:
					return UriModule.Parts;
				case 2:
					return UriModule.BuildQueryString;
				case 3:
					return UriModule.EscapeDataString;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x04000880 RID: 2176
		public static readonly FunctionValue Combine = new UriModule.CombineFunctionValue();

		// Token: 0x04000881 RID: 2177
		public static readonly FunctionValue Parts = new UriModule.PartsFunctionValue();

		// Token: 0x04000882 RID: 2178
		public static readonly FunctionValue BuildQueryString = new UriModule.BuildQueryStringFunctionValue();

		// Token: 0x04000883 RID: 2179
		public static readonly FunctionValue EscapeDataString = new UriModule.EscapeDataStringFunctionValue();

		// Token: 0x04000884 RID: 2180
		private static Keys exportKeys;

		// Token: 0x020002B5 RID: 693
		private enum Exports
		{
			// Token: 0x04000886 RID: 2182
			Combine,
			// Token: 0x04000887 RID: 2183
			Parts,
			// Token: 0x04000888 RID: 2184
			BuildQueryString,
			// Token: 0x04000889 RID: 2185
			EscapeDataString,
			// Token: 0x0400088A RID: 2186
			Count
		}

		// Token: 0x020002B6 RID: 694
		private sealed class CombineFunctionValue : NativeFunctionValue2<TextValue, TextValue, TextValue>
		{
			// Token: 0x06001B71 RID: 7025 RVA: 0x00039A05 File Offset: 0x00037C05
			public CombineFunctionValue()
				: base(TypeValue.Text, "baseUri", TypeValue.Text, "relativeUri", TypeValue.Text)
			{
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x00039A28 File Offset: 0x00037C28
			public override TextValue TypedInvoke(TextValue baseUri, TextValue relativeUri)
			{
				Uri uri = UriHelper.CreateAbsoluteUriFromValue(baseUri);
				Uri uri2 = UriHelper.CreateRelativeUriFromValue(relativeUri);
				if (uri.HostNameType == UriHostNameType.IPv6)
				{
					return TextValue.New(UriHelper.RemoveDefaultPort(UriHelper.CreateAbsoluteUriBuilderFromValue(TextValue.New(new Uri(uri, uri2).AbsoluteUri))));
				}
				return TextValue.New(UriHelper.CreateAbsoluteUriFromValue(TextValue.New(new Uri(uri, uri2).AbsoluteUri)).AbsoluteUri);
			}
		}

		// Token: 0x020002B7 RID: 695
		private sealed class PartsFunctionValue : NativeFunctionValue1<RecordValue, TextValue>
		{
			// Token: 0x06001B73 RID: 7027 RVA: 0x00039A8D File Offset: 0x00037C8D
			public PartsFunctionValue()
				: base(UriModule.PartsFunctionValue.returnType, "absoluteUri", TypeValue.Text)
			{
			}

			// Token: 0x06001B74 RID: 7028 RVA: 0x00039AA4 File Offset: 0x00037CA4
			public override RecordValue TypedInvoke(TextValue uri)
			{
				UriBuilder uriBuilder = UriHelper.CreateAbsoluteUriBuilderFromValue(uri);
				return RecordValue.New(UriModule.PartsFunctionValue.returnKeys, new Value[]
				{
					TextValue.New(uriBuilder.Scheme),
					TextValue.New(uriBuilder.Host),
					NumberValue.New(uriBuilder.Port),
					TextValue.New(uriBuilder.Path),
					UriHelper.CreateQueryRecord(uriBuilder.Query, false),
					TextValue.New(UriHelper.NormalizeUriComponent(uriBuilder.Fragment)),
					TextValue.New(uriBuilder.UserName),
					TextValue.New(uriBuilder.Password)
				});
			}

			// Token: 0x0400088B RID: 2187
			private static readonly Keys returnKeys = Keys.New(new string[] { "Scheme", "Host", "Port", "Path", "Query", "Fragment", "UserName", "Password" });

			// Token: 0x0400088C RID: 2188
			private static readonly RecordTypeValue returnType = RecordTypeValue.New(RecordValue.New(UriModule.PartsFunctionValue.returnKeys, new Value[]
			{
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Number,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					RecordTypeValue.Any,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				})
			}), false);
		}

		// Token: 0x020002B8 RID: 696
		private sealed class BuildQueryStringFunctionValue : NativeFunctionValue1<TextValue, RecordValue>
		{
			// Token: 0x06001B76 RID: 7030 RVA: 0x00039CD0 File Offset: 0x00037ED0
			public BuildQueryStringFunctionValue()
				: base(TypeValue.Text, 1, "query", TypeValue.Record)
			{
			}

			// Token: 0x06001B77 RID: 7031 RVA: 0x00039CE8 File Offset: 0x00037EE8
			public override TextValue TypedInvoke(RecordValue query)
			{
				return TextValue.New(UriHelper.BuildQueryString(query));
			}
		}

		// Token: 0x020002B9 RID: 697
		private sealed class EscapeDataStringFunctionValue : NativeFunctionValue1<TextValue, TextValue>
		{
			// Token: 0x06001B78 RID: 7032 RVA: 0x00039CF5 File Offset: 0x00037EF5
			public EscapeDataStringFunctionValue()
				: base(TypeValue.Text, 1, "data", TypeValue.Text)
			{
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x00039D0D File Offset: 0x00037F0D
			public override TextValue TypedInvoke(TextValue data)
			{
				return TextValue.New(UriMethods.EscapeDataString(data.AsString, false));
			}
		}
	}
}
