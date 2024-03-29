// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;

namespace Multi_Label_Classification.Model
{
    public class KnnModel
    {
        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"..\..\..\Data\iris-full.txt";

        public static int K { get; set; } = 3;
        public static ModelOutput Predict(ModelInput input)
        {
            var sortedDict = new SortedDictionary<double, temp>();
            var rows = GetDataSample(DATA_FILEPATH);
            foreach (var row in rows)
            {
                var itm1 = Math.Pow((input.Petal_length -  row.Petal_length),2);
                var itm2 = Math.Pow((input.Petal_width - row.Petal_width), 2);
                var itm3 = Math.Pow((input.Sepal_length - row.Sepal_length), 2);
                var itm4 = Math.Pow((input.Sepal_width- row.Sepal_width), 2);

                var total = itm1 + itm2 + itm3 + itm4;
                var dist = Math.Sqrt(total);
                if(!sortedDict.ContainsKey(dist)) sortedDict.Add(dist,new temp() { Label = row.Label, Result = dist });
            }
            
            var rankItems = sortedDict.Take(K);

            var ret = rankItems.GroupBy(n => n.Value.Label)
                         .Select(grp => new {
                            Label = grp.Key,
                            Count = grp.Count()
                        })
                        .OrderByDescending(n=>n.Count)
                        .FirstOrDefault();

            // Use model to make prediction on input data
            ModelOutput result = new ModelOutput() { Prediction = ret.Label, Score = ret.Count };
            return result;
        }

        private static IEnumerable<ModelInput> GetDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: '\t',
                                            allowQuoting: true,
                                            allowSparse: false);

            return mlContext.Data.CreateEnumerable<ModelInput>(dataView, false);
        }

        private class temp
        {
            public string Label { get; set; }
            public double Result { get; set; }
        }

    }
}
