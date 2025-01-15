using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000389 RID: 905
	internal class OpenApiOperationDefinition : OpenApiSpecObject
	{
		// Token: 0x06001FBC RID: 8124 RVA: 0x000527A9 File Offset: 0x000509A9
		public OpenApiOperationDefinition(RecordValue rawOperationDefinition, OpenApiDocument document, OpenApiPathItem pathItem)
			: base(rawOperationDefinition ?? RecordValue.Empty, document.UserSettings)
		{
			this.document = document;
			this.pathItem = pathItem;
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x000527CF File Offset: 0x000509CF
		public Value Tags
		{
			get
			{
				if (this.tags == null && !base.RawObject.TryGetValue("tags", out this.tags))
				{
					this.tags = Value.Null;
				}
				return this.tags;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x00052802 File Offset: 0x00050A02
		public Value Summary
		{
			get
			{
				if (this.summary == null && !base.RawObject.TryGetValue("summary", out this.summary))
				{
					this.summary = Value.Null;
				}
				return this.summary;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00052835 File Offset: 0x00050A35
		public Value Description
		{
			get
			{
				if (this.description == null && !base.RawObject.TryGetValue("description", out this.description))
				{
					this.description = Value.Null;
				}
				return this.description;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x00052868 File Offset: 0x00050A68
		public Value ExternalDocs
		{
			get
			{
				if (this.externalDocs == null && !base.RawObject.TryGetValue("externalDocs", out this.externalDocs))
				{
					this.externalDocs = Value.Null;
				}
				return this.externalDocs;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0005289C File Offset: 0x00050A9C
		public string OperationId
		{
			get
			{
				if (this.operationId == null)
				{
					Value value;
					this.operationId = (base.RawObject.TryGetValue("operationId", out value) ? value.AsString : string.Empty);
				}
				return this.operationId;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x000528E0 File Offset: 0x00050AE0
		public IList<string> Produces
		{
			get
			{
				if (this.produces == null)
				{
					this.produces = new List<string>();
					Value value;
					if (base.RawObject.TryGetValue("produces", out value))
					{
						OpenApiHelper.AppendToMimeList(this.produces, value, this.document.EngineHost);
					}
				}
				return this.produces;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00052934 File Offset: 0x00050B34
		public IList<OpenApiParameterDefinition> ParameterDefinitions
		{
			get
			{
				if (this.parameters == null)
				{
					IList<OpenApiParameterDefinition> list = new List<OpenApiParameterDefinition>();
					Value value;
					if (base.RawObject.TryGetValue("parameters", out value))
					{
						OpenApiHelper.AppendToResolvedParameterList(list, value.AsList, this.document);
					}
					if (this.pathItem != null && this.pathItem.Parameters != null)
					{
						using (IEnumerator<OpenApiParameterDefinition> enumerator = this.pathItem.Parameters.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								OpenApiParameterDefinition pathItemParameter = enumerator.Current;
								if (!list.Any((OpenApiParameterDefinition p) => p.Name == pathItemParameter.Name))
								{
									if (!(pathItemParameter.In != "body"))
									{
										if (list.Any((OpenApiParameterDefinition p) => p.In == "body"))
										{
											continue;
										}
									}
									list.Add(pathItemParameter);
								}
							}
						}
					}
					this.parameters = list;
				}
				return this.parameters;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00052A48 File Offset: 0x00050C48
		public IDictionary<string, OpenApiResponseDefinition> ResponseDefinitions
		{
			get
			{
				if (this.responseStatusToResponse == null)
				{
					this.responseStatusToResponse = new Dictionary<string, OpenApiResponseDefinition>();
					Value value;
					if (base.RawObject.TryGetValue("responses", out value))
					{
						foreach (NamedValue namedValue in value.AsRecord.GetFields())
						{
							RecordValue asRecord = namedValue.Value.AsRecord;
							this.responseStatusToResponse.Add(namedValue.Key, this.document.GetOrCreateResponse(asRecord));
						}
					}
				}
				return this.responseStatusToResponse;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x00052AFC File Offset: 0x00050CFC
		public bool? Deprecated
		{
			get
			{
				if (this.deprecated == null)
				{
					Value value;
					this.deprecated = new bool?(base.RawObject.TryGetValue("deprecated", out value) && value.AsBoolean);
				}
				return this.deprecated;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00052B44 File Offset: 0x00050D44
		public IList<IDictionary<string, IEnumerable<string>>> Security
		{
			get
			{
				if (this.security == null)
				{
					this.security = new List<IDictionary<string, IEnumerable<string>>>();
					Value value;
					if (base.RawObject.TryGetValue("security", out value))
					{
						this.securityGotSet = true;
						OpenApiHelper.AppendToSecurityList(this.security, value.AsList, this.document.EngineHost);
					}
				}
				if (!this.securityGotSet)
				{
					return null;
				}
				return this.security;
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00052BAC File Offset: 0x00050DAC
		protected override List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> metaDataRecords = base.GetMetaDataRecords();
			if (this.Tags != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("tags", this.Tags, TypeValue.Any));
			}
			if (this.Summary != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("summary", this.Summary, TypeValue.Text));
			}
			if (this.Description != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("description", this.Description, TypeValue.Text));
			}
			if (this.ExternalDocs != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("externalDocs", this.ExternalDocs, TypeValue.Any));
			}
			return metaDataRecords;
		}

		// Token: 0x04000BFB RID: 3067
		private readonly OpenApiDocument document;

		// Token: 0x04000BFC RID: 3068
		private readonly OpenApiPathItem pathItem;

		// Token: 0x04000BFD RID: 3069
		private string operationId;

		// Token: 0x04000BFE RID: 3070
		private bool? deprecated;

		// Token: 0x04000BFF RID: 3071
		private IList<string> produces;

		// Token: 0x04000C00 RID: 3072
		private IList<OpenApiParameterDefinition> parameters;

		// Token: 0x04000C01 RID: 3073
		private bool securityGotSet;

		// Token: 0x04000C02 RID: 3074
		private IList<IDictionary<string, IEnumerable<string>>> security;

		// Token: 0x04000C03 RID: 3075
		private Value tags;

		// Token: 0x04000C04 RID: 3076
		private Value summary;

		// Token: 0x04000C05 RID: 3077
		private Value description;

		// Token: 0x04000C06 RID: 3078
		private Value externalDocs;

		// Token: 0x04000C07 RID: 3079
		private IDictionary<string, OpenApiResponseDefinition> responseStatusToResponse;
	}
}
