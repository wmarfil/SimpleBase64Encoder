# Simple Base64 Encoder

This program runs a benchmark comparing the known dotnet implementation **Convert.ToBase64String** as a base with our own ones:

- **Base64.EncodeSafe** : uses StringBuilder to build the output.

- **Base64.Encode**: In a second attempt I went for an unsafe mode implementation with pointer manipulation, resulting in improved time and memory performance


### Reminders
 This encoder does not handle incomplet byte triplet, therefore its byte sequence input must be a multiple of 3.
Also the base64 character lookup table is slightly different that the actual base64 one.
