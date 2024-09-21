using PointLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FormsApp
{
    public class CustomSerializer
    {
        public void Serialize(StreamWriter writer, Point[] points)
        {
            var result = new StringBuilder();
            foreach (var point in points)
            {
                result.AppendLine($"[-] Type: {point.GetType().Name}, Coordinates: {point}\n");
            }
            writer.Write(result.ToString());
        }

        public Point[] Deserialize(string data)
        {
            var points = new List<Point>();
            var lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = new Regex(@"\[\-\] Type: (?<type>\w+), Coordinates: \((?<coords>[\d\s,]+)\)");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (!match.Success)
                {
                    continue;
                }

                var typeName = match.Groups["type"].Value;
                var coordinatesString = match.Groups["coords"].Value.Split(',');

                if (typeName.Equals("Point") && coordinatesString.Length == 2)
                {
                    var x = int.Parse(coordinatesString[0].Trim());
                    var y = int.Parse(coordinatesString[1].Trim());
                    points.Add(new Point(x, y));
                }
                else if (typeName.Equals("Point3D") && coordinatesString.Length == 3)
                {
                    var x = int.Parse(coordinatesString[0].Trim());
                    var y = int.Parse(coordinatesString[1].Trim());
                    var z = int.Parse(coordinatesString[2].Trim());
                    points.Add(new Point3D(x, y, z));
                }
            }

            return points.ToArray();
        }
    }
}
