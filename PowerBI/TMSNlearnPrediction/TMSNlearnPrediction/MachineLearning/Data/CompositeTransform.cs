using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000277 RID: 631
	public static class CompositeTransform
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0004CF73 File Offset: 0x0004B173
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CMPSTE F", 65537U, 65537U, 65537U, "CompositeRowFunction", null);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0004CF94 File Offset: 0x0004B194
		public static IDataTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(CompositeTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(input, "input");
			IDataTransform dataTransform = null;
			IHost host = env.Register("CompositeTransform");
			using (IChannel channel = host.Start("Loading Model"))
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num > 0, "Number of functions must be positive");
				for (int i = 0; i < num; i++)
				{
					string text = string.Format("Model_{0:000}", i);
					ctx.LoadModel<IDataTransform, SignatureLoadDataTransform>(out dataTransform, text, new object[] { env, input });
					input = dataTransform;
				}
				channel.Done();
			}
			return dataTransform;
		}

		// Token: 0x040007DB RID: 2011
		private const string RegistrationName = "CompositeTransform";

		// Token: 0x040007DC RID: 2012
		public const string LoaderSignature = "CompositeRowFunction";
	}
}
