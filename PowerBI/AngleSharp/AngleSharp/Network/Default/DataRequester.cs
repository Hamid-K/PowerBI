using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Extensions;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AA RID: 170
	public sealed class DataRequester : IRequester
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001F72F File Offset: 0x0001D92F
		public bool SupportsProtocol(string protocol)
		{
			return protocol.Is(ProtocolNames.Data);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001F73C File Offset: 0x0001D93C
		public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
		{
			MemoryStream memoryStream = new MemoryStream();
			string text = request.Address.Data;
			if (text.StartsWith(","))
			{
				text = MimeTypeNames.Plain + text;
			}
			string[] array = text.SplitCommas();
			Response response = new Response
			{
				Address = request.Address,
				Content = memoryStream,
				StatusCode = HttpStatusCode.BadRequest
			};
			if (array.Length == 2)
			{
				int num = array[0].IndexOf(DataRequester.Base64Section);
				bool flag = num >= 0;
				string text2 = (flag ? array[0].Remove(num, DataRequester.Base64Section.Length) : array[0]);
				try
				{
					byte[] array2 = (flag ? Convert.FromBase64String(array[1]) : array[1].UrlDecode());
					memoryStream.Write(array2, 0, array2.Length);
					memoryStream.Position = 0L;
					response.Headers.Add(HeaderNames.ContentType, text2);
					response.StatusCode = HttpStatusCode.OK;
				}
				catch (FormatException)
				{
				}
			}
			return TaskEx.FromResult<IResponse>(response);
		}

		// Token: 0x040003DA RID: 986
		private static readonly string Base64Section = ";base64";
	}
}
