using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200093A RID: 2362
	public static class MqResource
	{
		// Token: 0x06004346 RID: 17222 RVA: 0x000E3204 File Offset: 0x000E1404
		public static IResource New(MqConnectionParameters connectionParameters)
		{
			return MqResource.New((connectionParameters.Port == 0) ? string.Format(CultureInfo.InvariantCulture, "{0}", connectionParameters.Host) : string.Format(CultureInfo.InvariantCulture, "{0}:{1}", connectionParameters.Host, connectionParameters.Port), connectionParameters.QueueManager, connectionParameters.Channel, connectionParameters.Queue);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000E3268 File Offset: 0x000E1468
		public static IResource New(string server, string queueManager, string channel, string queue)
		{
			string text = MqResource.ToPath(new string[] { server, queueManager, channel, queue });
			return new Resource("MQ", text, text);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000E32A0 File Offset: 0x000E14A0
		public static string ToPath(params string[] parts)
		{
			return string.Join(';'.ToString(), parts);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000E32C0 File Offset: 0x000E14C0
		public static bool TryParsePath(string path, out string server, out string queuemanager, out string channel, out string queue)
		{
			if (path != null && MqResource.AreCharactersValid(path))
			{
				string[] array = path.Split(new char[] { ';' });
				if (array.Length == 4)
				{
					server = array[0];
					queuemanager = array[1];
					channel = array[2];
					queue = array[3];
					return true;
				}
			}
			server = null;
			queuemanager = null;
			channel = null;
			queue = null;
			return false;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000E3315 File Offset: 0x000E1515
		public static bool IsSubPath(string path, string candidateSubPath)
		{
			return candidateSubPath.StartsWith(path, StringComparison.Ordinal) && (candidateSubPath.Length == path.Length || MqResource.OccursExactlyOnceAtIndex(candidateSubPath, ';', path.Length));
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000E3341 File Offset: 0x000E1541
		private static bool OccursExactlyOnceAtIndex(string s, char c, int index)
		{
			return s.IndexOf(c) == index && s.IndexOf(c, index + 1) == -1;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000E335C File Offset: 0x000E155C
		private static bool AreCharactersValid(string path)
		{
			if (path.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < path.Length; i++)
			{
				if (char.IsControl(path[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002350 RID: 9040
		private const char Separator = ';';
	}
}
