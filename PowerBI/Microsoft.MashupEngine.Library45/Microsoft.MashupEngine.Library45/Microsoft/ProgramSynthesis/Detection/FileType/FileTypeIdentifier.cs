using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Detection.Encoding;
using Microsoft.ProgramSynthesis.Detection.FileType.Detectors;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType
{
	// Token: 0x02000ABA RID: 2746
	public class FileTypeIdentifier
	{
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x000D7C05 File Offset: 0x000D5E05
		internal static Dictionary<FileType, List<Type>> AllKnownDetectors
		{
			get
			{
				return FileTypeIdentifier.AllKnownDetectorsLazy.Value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x000D7C11 File Offset: 0x000D5E11
		internal static Dictionary<FileType, List<Type>> AllKnownBinaryDetectors
		{
			get
			{
				return FileTypeIdentifier.AllKnownBinaryDetectorsLazy.Value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x000D7C1D File Offset: 0x000D5E1D
		internal static Dictionary<FileType, List<Type>> AllKnownTextualDetectors
		{
			get
			{
				return FileTypeIdentifier.AllKnownTextualDetectorsLazy.Value;
			}
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x000D7C2C File Offset: 0x000D5E2C
		private static IEnumerable<Type> GetAllKnownSubclasses(Type baseType, bool skipAbstract = true)
		{
			return from t in typeof(FormatDetector).GetTypeInfo().Assembly.GetTypes()
				where baseType.GetTypeInfo().IsAssignableFrom(t) && (!skipAbstract || !t.GetTypeInfo().IsAbstract)
				select t;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x000D7C78 File Offset: 0x000D5E78
		private static Dictionary<FileType, List<Type>> GetAllKnownDetectors()
		{
			Dictionary<FileType, List<Type>> dictionary = new Dictionary<FileType, List<Type>>();
			foreach (Type type in FileTypeIdentifier.GetAllKnownSubclasses(typeof(FormatDetector), true))
			{
				FormatDetector formatDetector = Activator.CreateInstance(type) as FormatDetector;
				if (formatDetector == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not construct an object of type \"{0}\"", new object[] { type })));
				}
				foreach (FileType fileType in formatDetector.SupportedFileTypes)
				{
					dictionary.GetOrCreateValue(fileType).Add(type);
				}
			}
			return dictionary;
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x000D7D48 File Offset: 0x000D5F48
		private static Dictionary<FileType, List<Type>> FilterKnownDetectors(Type baseType)
		{
			Dictionary<FileType, List<Type>> dictionary = new Dictionary<FileType, List<Type>>();
			Func<Type, bool> <>9__0;
			foreach (KeyValuePair<FileType, List<Type>> keyValuePair in FileTypeIdentifier.AllKnownDetectors)
			{
				IEnumerable<Type> value = keyValuePair.Value;
				Func<Type, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Type t) => baseType.GetTypeInfo().IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract);
				}
				List<Type> list = value.Where(func).ToList<Type>();
				if (list.Count > 0)
				{
					dictionary[keyValuePair.Key] = list;
				}
			}
			return dictionary;
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000D7DF4 File Offset: 0x000D5FF4
		private static Dictionary<FileType, List<Type>> GetAllKnownBinaryDetectors()
		{
			return FileTypeIdentifier.FilterKnownDetectors(typeof(BinaryFormatDetector));
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000D7E05 File Offset: 0x000D6005
		private static Dictionary<FileType, List<Type>> GetAllKnownTextualDetectors()
		{
			return FileTypeIdentifier.FilterKnownDetectors(typeof(TextualFormatDetector));
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x000D7E16 File Offset: 0x000D6016
		internal Stream InputStream { get; }

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x000D7E1E File Offset: 0x000D601E
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x000D7E26 File Offset: 0x000D6026
		internal Encoding InputStreamEncoding { get; private set; }

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x000D7E2F File Offset: 0x000D602F
		// (set) Token: 0x060044F2 RID: 17650 RVA: 0x000D7E37 File Offset: 0x000D6037
		internal byte[] DataAsBinary { get; private set; }

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000D7E40 File Offset: 0x000D6040
		// (set) Token: 0x060044F4 RID: 17652 RVA: 0x000D7E48 File Offset: 0x000D6048
		internal string DataAsString { get; private set; }

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x000D7E51 File Offset: 0x000D6051
		// (set) Token: 0x060044F6 RID: 17654 RVA: 0x000D7E59 File Offset: 0x000D6059
		internal bool DetectEncoding { get; private set; }

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x000D7E62 File Offset: 0x000D6062
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x000D7E6A File Offset: 0x000D606A
		internal ISet<FileType> TypesToDetect { get; private set; }

		// Token: 0x060044F9 RID: 17657 RVA: 0x000D7E74 File Offset: 0x000D6074
		private void BuildStrings()
		{
			if (this.InputStreamEncoding == null)
			{
				if (!this.DetectEncoding)
				{
					return;
				}
				EncodingType encodingType = EncodingIdentifier.IdentifyEncoding(this.DataAsBinary, false);
				if (encodingType == EncodingType.Unknown)
				{
					return;
				}
				this.InputStreamEncoding = Encoding.GetEncoding(encodingType.GetDotNetName());
			}
			try
			{
				bool flag;
				this.DataAsString = this.InputStreamEncoding.GetStringWithoutBom(this.DataAsBinary, out flag).Normalize();
			}
			catch (Exception)
			{
				this.DataAsString = null;
			}
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000D7EF4 File Offset: 0x000D60F4
		private void BuildData()
		{
			if (this.InputStream != null)
			{
				this.DataAsBinary = new byte[Math.Min(this.InputStream.Length, 268435456L)];
				this.InputStream.RepeatRead(this.DataAsBinary, 0, this.DataAsBinary.Length);
				this.BuildStrings();
				return;
			}
			if (this.DataAsString != null)
			{
				this.DataAsBinary = Encoding.Unicode.GetBytes(this.DataAsString);
			}
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000D7F6C File Offset: 0x000D616C
		private Task<DetectionResult> MakeTask(Type detectorType, out FormatDetector detectorObject)
		{
			object detector = Activator.CreateInstance(detectorType);
			if (detector == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not instantiate detector of type: {0}.", new object[] { detectorType })));
			}
			detectorObject = detector as FormatDetector;
			if (this.TypesToDetect != null && !this.TypesToDetect.Intersect(detectorObject.SupportedFileTypes).Any<FileType>())
			{
				return null;
			}
			if (typeof(TextualFormatDetector).GetTypeInfo().IsAssignableFrom(detectorType))
			{
				if (this.DataAsString == null)
				{
					return null;
				}
				return new Task<DetectionResult>(delegate
				{
					TextualFormatDetector textualFormatDetector = (TextualFormatDetector)detector;
					return new DetectionResult(textualFormatDetector.MatchFormat(this, this.DataAsString, null), textualFormatDetector.Precedence);
				});
			}
			else
			{
				if (typeof(BinaryFormatDetector).GetTypeInfo().IsAssignableFrom(detectorType))
				{
					return new Task<DetectionResult>(delegate
					{
						BinaryFormatDetector binaryFormatDetector = (BinaryFormatDetector)detector;
						return new DetectionResult(binaryFormatDetector.MatchFormat(this, this.DataAsBinary, null), binaryFormatDetector.Precedence);
					});
				}
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not make file type detection task for detector type: {0}.", new object[] { detectorType })));
			}
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000D8064 File Offset: 0x000D6264
		private FileType IdentifyFromDetectors(IEnumerable<Type> detectorTypes)
		{
			List<Record<Task<DetectionResult>, FormatDetector>> list = new List<Record<Task<DetectionResult>, FormatDetector>>();
			foreach (Type type in detectorTypes)
			{
				FormatDetector formatDetector;
				Task<DetectionResult> task = this.MakeTask(type, out formatDetector);
				if (task != null)
				{
					list.Add(Record.Create<Task<DetectionResult>, FormatDetector>(task, formatDetector));
				}
			}
			foreach (IEnumerable<Record<Task<DetectionResult>, FormatDetector>> enumerable in from t in list
				group t by t.Item2.Precedence into g
				orderby g.Key
				select g)
			{
				List<Task<DetectionResult>> list2 = new List<Task<DetectionResult>>();
				foreach (Record<Task<DetectionResult>, FormatDetector> record in enumerable)
				{
					record.Item1.Start();
					list2.Add(record.Item1);
				}
				Task[] array = list2.Cast<Task>().ToArray<Task>();
				Task.WaitAll(array);
				Task[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DetectionResult result = ((Task<DetectionResult>)array2[i]).Result;
					if (result.FileType != FileType.Unknown && (this.TypesToDetect == null || this.TypesToDetect.Contains(result.FileType)))
					{
						return result.FileType;
					}
				}
			}
			return FileType.Unknown;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000D8218 File Offset: 0x000D6418
		private FileType Identify()
		{
			this.BuildData();
			FileType fileType;
			try
			{
				fileType = this.IdentifyFromDetectors(((this.DataAsBinary == null) ? FileTypeIdentifier.AllKnownTextualDetectors : FileTypeIdentifier.AllKnownDetectors).SelectMany((KeyValuePair<FileType, List<Type>> kvp) => kvp.Value).ConvertToHashSet<Type>());
			}
			catch (Exception)
			{
				fileType = FileType.Unknown;
			}
			return fileType;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000D828C File Offset: 0x000D648C
		private FileTypeIdentifier(Stream stream, Encoding encoding = null, bool detectEncoding = true, IEnumerable<FileType> typesToDetect = null)
		{
			this.InputStream = stream;
			this.InputStreamEncoding = encoding;
			this.DetectEncoding = detectEncoding;
			this.TypesToDetect = ((typesToDetect != null) ? typesToDetect.ConvertToHashSet<FileType>() : null);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000D82BD File Offset: 0x000D64BD
		private FileTypeIdentifier(string contents, IEnumerable<FileType> typesToDetect = null)
		{
			this.DataAsString = contents;
			this.TypesToDetect = ((typesToDetect != null) ? typesToDetect.ConvertToHashSet<FileType>() : null);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x000D82E0 File Offset: 0x000D64E0
		private static Record<EncodingType, FileType> Identify(Stream stream, string fileExtension, IEnumerable<FileType> typesToDetect = null)
		{
			EncodingType encodingType = EncodingIdentifier.IdentifyEncoding(stream, false);
			stream.Seek(0L, SeekOrigin.Begin);
			FileType fileType = FileTypeIdentifier.IdentifyFormat(stream, fileExtension, encodingType.GetEncoding(), false, typesToDetect);
			return new Record<EncodingType, FileType>(encodingType, fileType);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x000D8318 File Offset: 0x000D6518
		public static FileTypeInfo GetFileTypeInfo(string path, IEnumerable<FileType> typesToDetect = null)
		{
			FileTypeInfo fileTypeInfo;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				EncodingType encodingType;
				FileType fileType;
				FileTypeIdentifier.Identify(fileStream, Path.GetExtension(path), typesToDetect).Deconstruct(out encodingType, out fileType);
				EncodingType encodingType2 = encodingType;
				FileType fileType2 = fileType;
				fileTypeInfo = new FileTypeInfo(encodingType2, fileType2, path);
			}
			return fileTypeInfo;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000D8370 File Offset: 0x000D6570
		public static FileTypeInfo GetFileTypeInfo(Stream stream, string fileExtension = null, IEnumerable<FileType> typesToDetect = null)
		{
			EncodingType encodingType;
			FileType fileType;
			FileTypeIdentifier.Identify(stream, fileExtension, typesToDetect).Deconstruct(out encodingType, out fileType);
			EncodingType encodingType2 = encodingType;
			FileType fileType2 = fileType;
			return new FileTypeInfo(encodingType2, fileType2, stream);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000D8398 File Offset: 0x000D6598
		public static FileType IdentifyFormat(string path, Encoding encoding = null, bool detectEncoding = true, IEnumerable<FileType> typesToDetect = null)
		{
			FileType fileType;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				fileType = FileTypeIdentifier.IdentifyFormat(fileStream, Path.GetExtension(path), encoding, detectEncoding, typesToDetect);
			}
			return fileType;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000D83DC File Offset: 0x000D65DC
		public static FileType IdentifyFormat(Stream stream, string fileExtension = null, Encoding encoding = null, bool detectEncoding = true, IEnumerable<FileType> typesToDetect = null)
		{
			return new FileTypeIdentifier(stream, encoding, detectEncoding, typesToDetect).Identify();
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000D83ED File Offset: 0x000D65ED
		public static FileType IdentifyFormatFromContents(string contents, string fileExtension = null, IEnumerable<FileType> typesToDetect = null)
		{
			return new FileTypeIdentifier(contents, typesToDetect).Identify();
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000D83FC File Offset: 0x000D65FC
		public static FileType IdentifyFormatFromContents(byte[] contents, string fileExtension = null, Encoding encoding = null, bool detectEncoding = false, IEnumerable<FileType> typesToDetect = null)
		{
			FileType fileType;
			using (MemoryStream memoryStream = new MemoryStream(contents))
			{
				fileType = FileTypeIdentifier.IdentifyFormat(memoryStream, fileExtension, encoding, detectEncoding, typesToDetect);
			}
			return fileType;
		}

		// Token: 0x04001F84 RID: 8068
		private static readonly Lazy<Dictionary<FileType, List<Type>>> AllKnownDetectorsLazy = new Lazy<Dictionary<FileType, List<Type>>>(new Func<Dictionary<FileType, List<Type>>>(FileTypeIdentifier.GetAllKnownDetectors), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001F85 RID: 8069
		private static readonly Lazy<Dictionary<FileType, List<Type>>> AllKnownBinaryDetectorsLazy = new Lazy<Dictionary<FileType, List<Type>>>(new Func<Dictionary<FileType, List<Type>>>(FileTypeIdentifier.GetAllKnownBinaryDetectors), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001F86 RID: 8070
		private static readonly Lazy<Dictionary<FileType, List<Type>>> AllKnownTextualDetectorsLazy = new Lazy<Dictionary<FileType, List<Type>>>(new Func<Dictionary<FileType, List<Type>>>(FileTypeIdentifier.GetAllKnownTextualDetectors), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001F8D RID: 8077
		private const int MaxDataToRead = 268435456;
	}
}
