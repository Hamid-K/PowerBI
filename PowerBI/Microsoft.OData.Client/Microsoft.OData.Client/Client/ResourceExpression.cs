using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x0200009B RID: 155
	internal abstract class ResourceExpression : Expression
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x000119D4 File Offset: 0x0000FBD4
		internal ResourceExpression(Expression source, Type type, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: this(source, type, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion, null, null, false)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000119F8 File Offset: 0x0000FBF8
		internal ResourceExpression(Expression source, Type type, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion, string operationName, Dictionary<string, string> operationParameters, bool isAction)
		{
			this.source = source;
			this.type = type;
			this.expandPaths = expandPaths ?? new List<string>();
			this.countOption = countOption;
			this.customQueryOptions = customQueryOptions ?? new Dictionary<ConstantExpression, ConstantExpression>(ReferenceEqualityComparer<ConstantExpression>.Instance);
			this.projection = projection;
			this.ResourceTypeAs = resourceTypeAs;
			this.uriVersion = uriVersion ?? Util.ODataVersion4;
			this.operationName = operationName;
			this.OperationParameters = operationParameters ?? new Dictionary<string, string>(StringComparer.Ordinal);
			this.isAction = isAction;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00011A8E File Offset: 0x0000FC8E
		public override Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060004AA RID: 1194
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "type", Justification = "It is the value being used to set the field")]
		internal abstract ResourceExpression CreateCloneWithNewType(Type type);

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004AB RID: 1195
		internal abstract bool HasQueryOptions { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004AC RID: 1196
		internal abstract bool IsOperationInvocation { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004AD RID: 1197
		internal abstract Type ResourceType { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00011A96 File Offset: 0x0000FC96
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00011A9E File Offset: 0x0000FC9E
		internal Type ResourceTypeAs { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00011AA7 File Offset: 0x0000FCA7
		internal Version UriVersion
		{
			get
			{
				return this.uriVersion;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004B1 RID: 1201
		internal abstract bool IsSingleton { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00011AAF File Offset: 0x0000FCAF
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00011AB7 File Offset: 0x0000FCB7
		internal virtual List<string> ExpandPaths
		{
			get
			{
				return this.expandPaths;
			}
			set
			{
				this.expandPaths = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00011AC0 File Offset: 0x0000FCC0
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		internal virtual CountOption CountOption
		{
			get
			{
				return this.countOption;
			}
			set
			{
				this.countOption = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00011AD1 File Offset: 0x0000FCD1
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00011AD9 File Offset: 0x0000FCD9
		internal virtual Dictionary<ConstantExpression, ConstantExpression> CustomQueryOptions
		{
			get
			{
				return this.customQueryOptions;
			}
			set
			{
				this.customQueryOptions = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00011AE2 File Offset: 0x0000FCE2
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00011AEA File Offset: 0x0000FCEA
		internal ProjectionQueryOptionExpression Projection
		{
			get
			{
				return this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00011AF3 File Offset: 0x0000FCF3
		internal Expression Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00011AFB File Offset: 0x0000FCFB
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00011B03 File Offset: 0x0000FD03
		internal string OperationName
		{
			get
			{
				return this.operationName;
			}
			set
			{
				this.operationName = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00011B0C File Offset: 0x0000FD0C
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00011B14 File Offset: 0x0000FD14
		internal Dictionary<string, string> OperationParameters
		{
			get
			{
				return this.operationParameters;
			}
			set
			{
				this.operationParameters = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00011B1D File Offset: 0x0000FD1D
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00011B25 File Offset: 0x0000FD25
		internal bool IsAction
		{
			get
			{
				return this.isAction;
			}
			set
			{
				this.isAction = value;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00011B2E File Offset: 0x0000FD2E
		internal InputReferenceExpression CreateReference()
		{
			if (this.inputRef == null)
			{
				this.inputRef = new InputReferenceExpression(this);
			}
			return this.inputRef;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00011B4A File Offset: 0x0000FD4A
		internal void RaiseUriVersion(Version newVersion)
		{
			WebUtil.RaiseVersion(ref this.uriVersion, newVersion);
		}

		// Token: 0x04000210 RID: 528
		protected readonly Expression source;

		// Token: 0x04000211 RID: 529
		protected InputReferenceExpression inputRef;

		// Token: 0x04000212 RID: 530
		private Type type;

		// Token: 0x04000213 RID: 531
		private List<string> expandPaths;

		// Token: 0x04000214 RID: 532
		private CountOption countOption;

		// Token: 0x04000215 RID: 533
		private Dictionary<ConstantExpression, ConstantExpression> customQueryOptions;

		// Token: 0x04000216 RID: 534
		private ProjectionQueryOptionExpression projection;

		// Token: 0x04000217 RID: 535
		private Version uriVersion;

		// Token: 0x04000218 RID: 536
		private string operationName;

		// Token: 0x04000219 RID: 537
		private Dictionary<string, string> operationParameters;

		// Token: 0x0400021A RID: 538
		private bool isAction;
	}
}
