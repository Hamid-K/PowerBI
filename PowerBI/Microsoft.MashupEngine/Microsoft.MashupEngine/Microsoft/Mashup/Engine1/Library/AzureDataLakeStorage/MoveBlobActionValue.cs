using System;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC9 RID: 3785
	internal sealed class MoveBlobActionValue : ContinuationActionValue
	{
		// Token: 0x06006483 RID: 25731 RVA: 0x0015818A File Offset: 0x0015638A
		public MoveBlobActionValue(AdlsBinaryValue target, AdlsBinaryValue source)
		{
			this.target = target;
			this.source = source;
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x001581A0 File Offset: 0x001563A0
		public AdlsBinaryValue Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x001581A8 File Offset: 0x001563A8
		public AdlsBinaryValue Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x001581B0 File Offset: 0x001563B0
		protected override IEngineHost Host
		{
			get
			{
				return this.target.Host;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x001581BD File Offset: 0x001563BD
		protected override string TargetUri
		{
			get
			{
				return this.target.BlobUrl.AsString;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x001581D0 File Offset: 0x001563D0
		protected override IResource RequestResource
		{
			get
			{
				UriBuilder uriBuilder = new UriBuilder(this.target.BlobUrl.AsString);
				UriBuilder uriBuilder2 = new UriBuilder(this.source.BlobUrl.AsString);
				string[] array = uriBuilder.Path.Split(new char[] { '/' });
				string[] array2 = uriBuilder2.Path.Split(new char[] { '/' });
				int num = MoveBlobActionValue.IndexOfLastCommonItem(array, array2);
				UriBuilder uriBuilder3 = new UriBuilder();
				uriBuilder3.Scheme = uriBuilder.Scheme;
				uriBuilder3.Host = uriBuilder.Host;
				uriBuilder3.Port = uriBuilder.Port;
				uriBuilder3.Path = string.Join("/", array.Take(num + 1).ToArray<string>());
				return Resource.New(this.target.Resource.Kind, uriBuilder3.Uri.AbsoluteUri);
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06006489 RID: 25737 RVA: 0x001582AF File Offset: 0x001564AF
		protected override string Method
		{
			get
			{
				return "PUT";
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x001582B8 File Offset: 0x001564B8
		protected override Value Headers
		{
			get
			{
				if (this.headers == null)
				{
					ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.Host, this.RequestResource, null);
					UriBuilder uriBuilder = new UriBuilder(this.Source.BlobUrl.AsString);
					HttpResourceCredentialDispatcher.ApplyCredentialsToUri(uriBuilder, null, resourceCredentialCollection, this.Host);
					RecordBuilder recordBuilder = new RecordBuilder(3);
					recordBuilder.Add("x-ms-rename-source", TextValue.New(uriBuilder.Uri.PathAndQuery), TypeValue.Text);
					bool flag = this.target.MustNotExistToCreate;
					if (!flag && this.target.Version != null)
					{
						string trackedUrlETag = this.target.Version.GetTrackedUrlETag(this.target.BlobUrl.AsString);
						flag = trackedUrlETag == null;
						if (!flag)
						{
							recordBuilder.Add("If-Match", TextValue.New(trackedUrlETag), TypeValue.Text);
						}
					}
					if (flag)
					{
						recordBuilder.Add("If-None-Match", TextValue.New("*"), TypeValue.Text);
					}
					if (this.source.Version != null)
					{
						recordBuilder.Add("x-ms-source-if-match", TextValue.New(this.source.Version.GetTrackedUrlETag(this.source.BlobUrl.AsString)), TypeValue.Text);
					}
					this.headers = recordBuilder.ToRecord();
				}
				return this.headers;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x0600648B RID: 25739 RVA: 0x00158404 File Offset: 0x00156604
		protected override bool IsOneLake
		{
			get
			{
				return this.Source.Options.GetBool("IsOneLake", false);
			}
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x0015841C File Offset: 0x0015661C
		private static int IndexOfLastCommonItem(string[] array1, string[] array2)
		{
			int num = -1;
			int num2 = 0;
			while (num2 < array1.Length && num2 < array2.Length && !(array1[num2] != array2[num2]))
			{
				num++;
				num2++;
			}
			return num;
		}

		// Token: 0x040036EB RID: 14059
		private readonly AdlsBinaryValue target;

		// Token: 0x040036EC RID: 14060
		private readonly AdlsBinaryValue source;

		// Token: 0x040036ED RID: 14061
		private RecordValue headers;
	}
}
