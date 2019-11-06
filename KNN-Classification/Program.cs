using Multi_Label_Classification.Model;
using System;

namespace Multi_Label_Classification
{
    class Program
    {
      
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput
            {
                Label = "Iris-setosa",
                Sepal_length = 5.1f,
                Sepal_width = 3.5f,
                Petal_length = 1.6f,
                Petal_width = 0.4f,
            };

            // Make a single prediction on the sample data and print results
            KnnModel.K = 3;
            ModelOutput predictionResult = KnnModel.Predict(sampleData);

            Console.WriteLine("Using KNN model to make single prediction\n");
            Console.WriteLine($"Parameter K:{KnnModel.K}\n\n");
            Console.WriteLine($"Sepal length: {sampleData.Sepal_length}");
            Console.WriteLine($"Sepal width: {sampleData.Sepal_width}");
            Console.WriteLine($"Petal length: {sampleData.Petal_length}");
            Console.WriteLine($"Petal width: {sampleData.Petal_width}");
            Console.WriteLine($"Actual Label: {sampleData.Label} \n\nPredicted Label: {predictionResult.Prediction} \nPredicted Label Count: {String.Join(",", predictionResult.Score)}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

    }
}
