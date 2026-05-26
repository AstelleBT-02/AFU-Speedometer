# AFU-Speedometer
Add's a center screen speedometer which displays ground speed in a more truthful unit than the hud speedometer's "kmh"

The unit is deci-units per second, or du/s. It is measured by:
1. Subracting your last position from your current position, which is stored in Unity's non descript unit of distance measurment
2. Removing the Y component
3. Getting the magnitude of the vector
4. Multiplying it by 45 to get units per second
5. Multiplying it by 10 to get deci-units per second

We are not certain of how kmh is calculated but it does seem to be inflated to make things feel faster; Lower speeds are lower and higher speeds are higher. 300kmh is about 280du/s. Displaying in kmh is planned eventually; du/s was made just because it was simple to calculate.
