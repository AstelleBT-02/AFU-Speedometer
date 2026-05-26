# AFU-Speedometer
Add's a center screen speedometer which displays ground speed in a more truthful unit than the hud speedometer's "kmh"

The unit is centi-units per tick, or cu/t. It is measured by:
1. Subracting your last position from your current position, which is stored in Unity's non descript unit of distance measurment
2. Removing the Y component
3. Getting the magnitude of the vector
4. Multiplying it by 100

Technically this is cheating a little bit since the speedometer displays the 1's place in a smaller font, which you would think is the tenths place but no. Deci-units per tick is such a worse acronym as du/t so i fudged the numbers to a cooler name but displayed them in du/t for the sake of usability.

More important than naming conventions is how it compares to the hud's kmh unit. We are not certain of how kmh is calculated but it does seem to be inflated to make things feel faster; Lower speeds are lower and higher speeds are higher. 300kmh is about 2800cu/t. 
