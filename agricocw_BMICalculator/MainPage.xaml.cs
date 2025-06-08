using System;
using Microsoft.Maui.Controls;

namespace agricocw_BMICalculator
{
    public partial class MainPage : ContentPage
    {
        private string selectedGender = ""; // Will be "Male" or "Female"

        public MainPage()
        {
            InitializeComponent();
            UpdateSliderLabels();
        }

        // Update height label in real-time
        private void HeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            HeightLabel.Text = $"Height: {Math.Round(e.NewValue)} in";
        }

        // Update weight label in real-time
        private void WeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            WeightLabel.Text = $"Weight: {Math.Round(e.NewValue)} lbs";
        }

        // Handle Male tap
        private void OnMaleTapped(object sender, EventArgs e)
        {
            selectedGender = "Male";
            MaleFrame.BorderColor = Colors.Blue;
            FemaleFrame.BorderColor = Colors.Gray;
        }

        // Handle Female tap
        private void OnFemaleTapped(object sender, EventArgs e)
        {
            selectedGender = "Female";
            FemaleFrame.BorderColor = Colors.Pink;
            MaleFrame.BorderColor = Colors.Gray;
        }

        // Handle Calculate BMI button click
        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedGender))
            {
                await DisplayAlert("Error", "Please select a gender.", "OK");
                return;
            }

            double height = Math.Round(HeightSlider.Value);
            double weight = Math.Round(WeightSlider.Value);

            if (height == 0)
            {
                await DisplayAlert("Error", "Height must be greater than 0.", "OK");
                return;
            }

            double bmi = (weight * 703) / (height * height);
            double roundedBmi = Math.Round(bmi);

            string status = GetHealthStatus(selectedGender, bmi);
            string recommendations = GetRecommendations(status);

            await DisplayAlert("Your calculated BMI results are:",
                $"Gender: {selectedGender}\nBMI: {roundedBmi}\nHealth Status: {status}\nRecommendations:\n{recommendations}",
                "Ok");
        }

        private string GetHealthStatus(string gender, double bmi)
        {
            if (gender == "Male")
            {
                if (bmi < 18.5) return "Underweight";
                if (bmi < 25) return "Normal weight";
                if (bmi < 30) return "Overweight";
                return "Obese";
            }
            else // Female
            {
                if (bmi < 18) return "Underweight";
                if (bmi < 24) return "Normal weight";
                if (bmi < 29) return "Overweight";
                return "Obese";
            }
        }

        private string GetRecommendations(string status)
        {
            switch (status)
            {
                case "Underweight":
                    return "- Increase nutrient-rich foods\n- Strength training\n- Consult a nutritionist";
                case "Normal weight":
                    return "- Maintain a balanced diet\n- Exercise at least 150 min/week\n- Monitor health regularly";
                case "Overweight":
                    return "- Reduce processed foods\n- Portion control\n- Aerobic + strength exercises\n- Stay hydrated";
                case "Obese":
                    return "- Consult a doctor\n- Low-impact exercise (walking, cycling)\n- Weight-loss plan\n- Improve sleep and reduce sugar";
                default:
                    return "";
            }
        }

        private void UpdateSliderLabels()
        {
            HeightLabel.Text = $"Height: {Math.Round(HeightSlider.Value)} in";
            WeightLabel.Text = $"Weight: {Math.Round(WeightSlider.Value)} lbs";
        }
    }
}

