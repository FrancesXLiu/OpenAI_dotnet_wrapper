using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ImageVariationRequestModel : ImageRequestBaseModel
{
    /// <summary>
    /// The image to use as the basis for the variation(s). Must be a valid PNG file, less than 4MB, and square.
    /// </summary>
    public byte[] Image { get; set; }
    public string ImageName { get; set; }
}
